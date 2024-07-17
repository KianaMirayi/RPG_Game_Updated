using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major Stats")]
    public Stats Strength;  // 每增加一点就可以提升1点的暴击伤害
    public Stats Agility;   // 每增加一点就可以提升1点的闪避和反击
    public Stats Intelligence;  //每增加一点就可以提升1点的魔法伤害
    public Stats Vitality;  // 每增加一点就可以提升3点-5点的Hp

    [Header("Offensive Stats")]
    public Stats Damage;//物体造成的伤害
    public Stats CritChance;//暴击率
    public Stats CritPower;  //基础暴击伤害默认150


    [Header("Defensive Stats")]
    public Stats MaxHp;
    public Stats Armor;
    public Stats Evasion;
    public Stats MagicResistance;

    [Header("Magic Stats")]
    public Stats FireDamage;
    public Stats IceDamage;
    public Stats LightingDamage;

    public bool IsIgnited;  //持续期间受到火焰伤害
    public bool IsChilled;  //持续期间护甲值减少
    public bool IsShocked;  //持续期间命中率下降

    [Header("Element info")]
    private float IgniteTimer;
    private float ChillTimer;
    private float ShockTImer;
    public float ElementDuration;
    private float IgniteDamageFrequency = 0.3f;  //每0.3秒受到一次点燃伤害
    private float IgniteDamageTimer;
    private int IgniteDamage;
    public int ShockDamage;
    [SerializeField] GameObject ThunderStrikePrefab;


    public int CurrentHp;

    public System.Action onHpUpdate;  //委托 生命值实时更新

    public bool IsDead = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        CritPower.SetDefaultValue(150);
        CurrentHp = GetMaxHp() ;

        fx = GetComponent<EntityFX>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        IgniteTimer -= Time.deltaTime;
        ChillTimer -= Time.deltaTime;
        ShockTImer -= Time.deltaTime;

        IgniteDamageTimer -= Time.deltaTime;

        if (IgniteTimer < 0)  //点燃时间为4秒
        {
            IsIgnited = false;
        }

        if (ChillTimer < 0)
        {
            IsChilled = false;
        }

        if (ShockTImer < 0)
        {
            IsShocked = false;
        }
        if (IsIgnited)
        {
            ApplyIgniteDamage();
        }
    }

    public virtual void IncreaseStatBy(int _modifier, float _duration, Stats _statToModify)
    {
        StartCoroutine(StartModifyCoroutine(_modifier,_duration,_statToModify));
    }

    private IEnumerator StartModifyCoroutine(int _modifier, float _duration, Stats _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }


    public virtual void DoDamage(CharacterStats _target)  //物理伤害
    {
        if (TargetCanAvoidAttack(_target))
        {
            return;
        }

        
      
            int totalDamage = Damage.GetValue() + Strength.GetValue();  //总伤害 = 基础伤害 + 力量加点

            if (CanCrit())
            {
                //Debug.Log("Critical HIT");
                totalDamage = CalculateCriticalDamage(totalDamage);
                //Debug.Log("Total Critical Damage is" + totalDamage);
            }

            totalDamage = CheckTargetArmor(_target, totalDamage);

            _target.TakeDamage(totalDamage);
            DoMagicDamage(_target);
        


    }

    #region Elemental or Magical Damage
    public virtual void DoMagicDamage(CharacterStats _target)  //魔法伤害
    {
        int fireDamage = FireDamage.GetValue();
        int iceDamage = IceDamage.GetValue();
        int lightingDamage = LightingDamage.GetValue();


        int totalMagicDamage = fireDamage + iceDamage + lightingDamage + Intelligence.GetValue(); // 计算总魔法伤害

        totalMagicDamage = CheckTargetResistance(_target, totalMagicDamage);

        _target.TakeDamage(totalMagicDamage);




        if (Mathf.Max(fireDamage, iceDamage, lightingDamage) <= 0)  //当三种属性的伤害都为0时，直接终止以后的代码避免造成死循环
        {
            return;

        }

        TryToApplyElement(_target, fireDamage, iceDamage, lightingDamage);

    }

    private void TryToApplyElement(CharacterStats _target, int fireDamage, int iceDamage, int lightingDamage)
    {
        bool CanApplyIgnite = fireDamage > iceDamage && fireDamage > lightingDamage;
        bool CanApplyChill = iceDamage > fireDamage && iceDamage > lightingDamage;
        bool CanApplyShock = lightingDamage > fireDamage && lightingDamage > iceDamage;


        while (!CanApplyIgnite && !CanApplyChill && !CanApplyShock)  //当火焰、冰冻、禁锢属性伤害相同时，随机造成一次属性伤害
        {
            if (Random.value < 0.33f && fireDamage > 0)  //0.33 0.5 1的概率分布使三种属性伤害出现更加平均，因为逐行读取代码，若三种属性伤害概率一样，那么排在第一个的属性出现次数将会更多
            {
                CanApplyIgnite = true;
                _target.ApplyElement(CanApplyIgnite, CanApplyChill, CanApplyShock);
                //Debug.Log("Fire Applied");
                return;
            }

            if (Random.value < 0.5f && iceDamage > 0)
            {
                CanApplyChill = true;
                _target.ApplyElement(CanApplyIgnite, CanApplyChill, CanApplyShock);
                //Debug.Log("Ice Applied");
                return;
            }

            if (Random.value < 1 && lightingDamage > 0)
            {
                CanApplyShock = true;
                _target.ApplyElement(CanApplyIgnite, CanApplyChill, CanApplyShock);
                //Debug.Log("Shock Applied");
                return;
            }

        }

        if (CanApplyIgnite)
        {
            _target.SetUpIgniteDamage(Mathf.RoundToInt(fireDamage * 0.2f));
        }

        if (CanApplyShock)
        {
            _target.SetUpShockThunderStrikeDamage(Mathf.RoundToInt(lightingDamage * 0.2f));
        }

        _target.ApplyElement(CanApplyIgnite, CanApplyChill, CanApplyShock);
    }

    public void ApplyElement(bool _ignite, bool _chill, bool _shock)
    {
        //if (IsIgnited || IsChilled || IsShocked)
        //{ 
        //    return ;
        //}

        bool canApplyIgnite = !IsIgnited && !IsChilled && !IsShocked;
        bool canApplyChill = !IsIgnited && !IsChilled && !IsShocked;
        bool canApplyShock = !IsIgnited && !IsChilled;

        if (_ignite && canApplyIgnite)
        { 
            IsIgnited = _ignite;
            IgniteTimer = ElementDuration;  

            fx.IgniteFxFor(ElementDuration);
        }

        if (_chill && canApplyChill)
        { 
            IsChilled = _chill;
            ChillTimer = ElementDuration;

            float _slowpercentage = 0.2f;
            GetComponent<Entity>().SlowEntityBy(_slowpercentage, ElementDuration);
            fx.ChillFxFor(ElementDuration);
        }

        if (_shock && canApplyShock)
        {
            if (IsShocked)  //对当前的敌人产生雷击伤害
            {
                GameObject newThunderStrike = Instantiate(ThunderStrikePrefab, transform.position, Quaternion.identity);

                newThunderStrike.GetComponent<ThunderStrike_Controller>().SetUpThunder(ShockDamage, transform.GetComponent<CharacterStats>());
            }
            

            if (!IsShocked)  //对当前敌人旁边的敌人也可以造成累计伤害
            {
                ApplyShock(_shock);

            }
            else
            {
                if (GetComponent<Player>() != null)
                {
                    return;
                }
                HitNearestTargetWithThunderStrike();
            }

        }
 
    }

    private void ApplyIgniteDamage()
    {
        if (IgniteDamageTimer < 0)  //当在点燃持续期间时
        {
            //Debug.Log("Take Burning " + IgniteDamage);

            //CurrentHp -= IgniteDamage;
            DecreaseHpBy(IgniteDamage);
            if (CurrentHp < 0 && !IsDead)
            {
                Die();
            }

            IgniteDamageTimer = IgniteDamageFrequency;
        }
    }

    public void ApplyShock(bool _shock)
    {
        if (IsShocked)
        {
            return;
        }

        IsShocked = _shock;
        ShockTImer = ElementDuration;
        fx.ShockFxFor(ElementDuration);
    }


    public void SetUpIgniteDamage(int _ignitedamage)
    {
        IgniteDamage = _ignitedamage;
    }

    public void SetUpShockThunderStrikeDamage(int _thunderStrikeDamage)
    {
        ShockDamage = _thunderStrikeDamage;
    }

    private void HitNearestTargetWithThunderStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 7);

        float closetDistance = Mathf.Infinity;

        Transform ClosetEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    ClosetEnemy = hit.transform;
                }
            }
        }

        if (ClosetEnemy != null)
        {
            GameObject newThunderStrike = Instantiate(ThunderStrikePrefab, transform.position, Quaternion.identity);

            newThunderStrike.GetComponent<ThunderStrike_Controller>().SetUpThunder(ShockDamage, ClosetEnemy.GetComponent<CharacterStats>());
        }

        //if (ClosetEnemy == null)
        //{
        //    ClosetEnemy = transform;
        //}
    }

    #endregion

    #region Calculate Damage
    private int CheckTargetResistance(CharacterStats _target, int totalMagicDamage)  //角色属性抗性
    {
        totalMagicDamage -= _target.MagicResistance.GetValue() + (_target.Intelligence.GetValue() * 3); //计算总魔法伤害受抗性减免后的总伤害
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    private int CheckTargetArmor(CharacterStats _target, int totalDamage)  //角色护甲值
    {
        if (_target != null)
        {

            if (_target.IsChilled)  //若受到冰属性伤害，则造成的伤害降低
            {
                totalDamage -= Mathf.RoundToInt(_target.Armor.GetValue() * 0.8f);
            }
            else
            {
                totalDamage -= _target.Armor.GetValue();  //获取当前物体护甲值，最后伤害为总伤害 - 护甲
            }

            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);  //避免伤害为负导致加血
        }
            return totalDamage;

    }

    private bool TargetCanAvoidAttack(CharacterStats _target)  // 角色的闪避概率
    {
        if (_target != null)
        {
            int totalEvasion = _target.Evasion.GetValue() + _target.Agility.GetValue();  //总闪避 = 闪避 + 敏捷加点

            if (IsShocked)  //若敌人被雷击，则角色闪避概率增加20
            {
                totalEvasion += 20;
            }

            if (Random.Range(0, 100) < totalEvasion)
            {
                //Debug.Log("Attack avoided");
                return true;
            }
        }

        
        return false;
    }

    

    public virtual void TakeDamage(int _damage)  //角色受到伤害
    { 
        //CurrentHp -= _damage;
        DecreaseHpBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFx");

        if (CurrentHp <= 0 && !IsDead)
        {
            Die();
        }
    }

    public virtual void DecreaseHpBy(int _damage)
    {
        CurrentHp -= _damage;

        if (onHpUpdate != null)
        {
            onHpUpdate();
        }

    }


    public virtual void IncreaseHpBy(int _healAmount)
    { 
        CurrentHp += _healAmount;

        if (CurrentHp > GetMaxHp())
        { 
            CurrentHp = GetMaxHp();
        }

        if (onHpUpdate != null)
        {
            onHpUpdate();
        }
    }

    public virtual void Die()
    {
        IsDead = true;
    }

    public bool CanCrit()  //计算暴击率
    {
        int totalCriticalChance = CritChance.GetValue() + Agility.GetValue(); // 暴击率 = 基础暴击率 + 敏捷加点

        if (Random.Range(0, 100) <= totalCriticalChance)
        { 
            return true;
        }
       
        return false;
    }

    public int CalculateCriticalDamage(int _damage)  //计算暴击伤害
    {
        float totalCritPower = (CritPower.GetValue() + Strength.GetValue()) * 0.01f; //暴击倍率 = （基础暴击伤害 + 力量加点）* 1%
        //Debug.Log("total crit power " + totalCritPower);

        float critDamage = _damage * totalCritPower;  //暴击伤害 = 普攻伤害 * 暴击倍率
        //Debug.Log("crit damage before round up " + critDamage);

        return Mathf.RoundToInt(critDamage);
    }

    #endregion


    public int GetMaxHp()  //获取角色最大生命值并之后分配给UI
    {
        return MaxHp.GetValue() + Vitality.GetValue() * 5;
    }
}
