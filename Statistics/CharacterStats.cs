using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicalResistance,
    fireDamage,
    iceDamage,
    lightingDamage
}
public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major Stats")]
    public Stats Strength;  // ÿ����һ��Ϳ�������1��ı����˺�
    public Stats Agility;   // ÿ����һ��Ϳ�������1������ܺͱ�����
    public Stats Intelligence;  //ÿ����һ��Ϳ�������3���ħ������
    public Stats Vitality;  // ÿ����һ��Ϳ�������5���Hp

    [Header("Offensive Stats")]
    public Stats Damage;//������ɵ��˺�
    public Stats CritChance;//������
    public Stats CritPower;  //���������˺�Ĭ��150


    [Header("Defensive Stats")]
    public Stats MaxHp;
    public Stats Armor;
    public Stats Evasion; //���ܿ���ͨ���������
    public Stats MagicResistance;

    [Header("Magic Stats")]
    public Stats FireDamage;
    public Stats IceDamage;
    public Stats LightingDamage;

    public bool IsIgnited;  //�����ڼ��ܵ������˺�
    public bool IsChilled;  //�����ڼ令��ֵ����
    public bool IsShocked;  //�����ڼ��������½�

    [Header("Element info")]
    private float IgniteTimer;
    private float ChillTimer;
    private float ShockTImer;
    public float ElementDuration;
    private float IgniteDamageFrequency = 0.3f;  //ÿ0.3���ܵ�һ�ε�ȼ�˺�
    private float IgniteDamageTimer;
    private int IgniteDamage;
    public int ShockDamage;
    [SerializeField] GameObject ThunderStrikePrefab;


    public int CurrentHp;

    public System.Action onHpUpdate;  //ί�� ����ֵʵʱ����

    public bool IsDead = false;

    public bool IsVulnerable;

    public bool isInvincible; //�޵�״̬

    private void Awake()
    {
        fx = GetComponent<EntityFX>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        CritPower.SetDefaultValue(150);
        CurrentHp = GetMaxHp() ;

        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        IgniteTimer -= Time.deltaTime;
        ChillTimer -= Time.deltaTime;
        ShockTImer -= Time.deltaTime;

        IgniteDamageTimer -= Time.deltaTime;

        if (IgniteTimer < 0)  //��ȼʱ��Ϊ4��
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

    public void MakeVulnerableFor(float _duration)
    {
        StartCoroutine(VulnerableCoroutine(_duration));
    }

    public IEnumerator VulnerableCoroutine(float _duration)  //����������ʱ������Э��
    { 
        IsVulnerable = true;

        yield return new WaitForSeconds(_duration);

        IsVulnerable = false;
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


    public virtual void DoDamage(CharacterStats _target)  //�˺�
    {
        bool criticalHit = false;

        if (TargetCanAvoidAttack(_target))
        {
            return;
        }

        _target.GetComponent<Entity>().SetUpKnockDir(transform);
        Debug.Log("11111");
      
            int totalDamage = Damage.GetValue() + Strength.GetValue();  //���˺� = �����˺� + �����ӵ�

            if (CanCrit())
            {
                //Debug.Log("Critical HIT");
                totalDamage = CalculateCriticalDamage(totalDamage);
                //Debug.Log("Total Critical Damage is" + totalDamage);

                criticalHit = true;
                fx.ScreenShake(fx.critHitShakeImpact); //��ͷ����
            }

        fx.GenerateHitFX(_target.transform,criticalHit); //�����뱩����Ч
        


            totalDamage = CheckTargetArmor(_target, totalDamage);

            _target.TakeDamage(totalDamage);
            DoMagicDamage(_target);
        


    }

    #region Elemental or Magical Damage
    public virtual void DoMagicDamage(CharacterStats _target)  //ħ���˺�
    {
        int fireDamage = FireDamage.GetValue();
        int iceDamage = IceDamage.GetValue();
        int lightingDamage = LightingDamage.GetValue();


        int totalMagicDamage = fireDamage + iceDamage + lightingDamage + Intelligence.GetValue(); // ������ħ���˺�

        totalMagicDamage = CheckTargetResistance(_target, totalMagicDamage);

        _target.TakeDamage(totalMagicDamage);




        if (Mathf.Max(fireDamage, iceDamage, lightingDamage) <= 0)  //���������Ե��˺���Ϊ0ʱ��ֱ����ֹ�Ժ�Ĵ�����������ѭ��
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


        while (!CanApplyIgnite && !CanApplyChill && !CanApplyShock)  //�����桢���������������˺���ͬʱ��������һ�������˺�
        {
            if (Random.value < 0.33f && fireDamage > 0)  //0.33 0.5 1�ĸ��ʷֲ�ʹ���������˺����ָ���ƽ������Ϊ���ж�ȡ���룬�����������˺�����һ������ô���ڵ�һ�������Գ��ִ����������
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
            if (IsShocked)  //�Ե�ǰ�ĵ��˲����׻��˺�
            {
                GameObject newThunderStrike = Instantiate(ThunderStrikePrefab, transform.position, Quaternion.identity);

                newThunderStrike.GetComponent<ThunderStrike_Controller>().SetUpThunder(ShockDamage, transform.GetComponent<CharacterStats>());
            }
            

            if (!IsShocked)  //�Ե�ǰ�����Աߵĵ���Ҳ��������ۼ��˺�
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
        if (IgniteDamageTimer < 0)  //���ڵ�ȼ�����ڼ�ʱ
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
    private int CheckTargetResistance(CharacterStats _target, int totalMagicDamage)  //��ɫ���Կ���
    {
        totalMagicDamage -= _target.MagicResistance.GetValue() + (_target.Intelligence.GetValue() * 3); //������ħ���˺��ܿ��Լ��������˺�
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    protected int CheckTargetArmor(CharacterStats _target, int totalDamage)  //��ɫ����ֵ
    {
        if (_target != null)
        {

            if (_target.IsChilled)  //���ܵ��������˺�������ɵ��˺�����
            {
                totalDamage -= Mathf.RoundToInt(_target.Armor.GetValue() * 0.8f);
            }
            else
            {
                totalDamage -= _target.Armor.GetValue();  //��ȡ��ǰ���廤��ֵ������˺�Ϊ���˺� - ����
            }

            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);  //�����˺�Ϊ�����¼�Ѫ
        }
            return totalDamage;

    }

    public virtual void OnEvasion()
    { 
        
    }

    protected bool TargetCanAvoidAttack(CharacterStats _targetStats)  // ��ɫ�����ܸ���
    {
        if (_targetStats != null)
        {
            int totalEvasion = _targetStats.Evasion.GetValue() + _targetStats.Agility.GetValue();  //������ = ���� + ���ݼӵ�

            if (IsShocked)  //�����˱��׻������ɫ���ܸ�������20
            {
                totalEvasion += 20;
            }

            if (Random.Range(0, 100) < totalEvasion)
            {
                //Debug.Log("Attack avoided");
                _targetStats.OnEvasion();
                return true;
            }
        }

        
        return false;
    }

    

    public virtual void TakeDamage(int _damage)  //�ܵ��˺�
    {
        if (isInvincible) //�޵��������˺�
        {
            return;
        }

        //CurrentHp -= _damage;
        DecreaseHpBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFx"); //�ܻ���Ч����Э��

        if (CurrentHp <= 0 && !IsDead)
        {
            Die();
        }
    }

    public virtual void DecreaseHpBy(int _damage)
    {
        if (IsVulnerable)
        {
            _damage = Mathf.RoundToInt(_damage * 1.1f); // ������ʱ���յ����˺�Ϊ110%
        }

        CurrentHp -= _damage;

        if (_damage > 0)
        {
            fx.CreatePopUpText(_damage.ToString()); //��ӡ�˺�����
        }


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


    public void KillEntity()//�����������������ҵ���͸
    {
        if (!IsDead)
        {
            Die();
        }
    }

    public void MakeInvincible(bool _invincible)
    {
        isInvincible = _invincible;
    }

    public bool CanCrit()  //���㱩����
    {
        int totalCriticalChance = CritChance.GetValue() + Agility.GetValue(); // ������ = ���������� + ���ݼӵ�

        if (Random.Range(0, 100) <= totalCriticalChance)
        { 
            return true;
        }
       
        return false;
    }

    public int CalculateCriticalDamage(int _damage)  //���㱩���˺�
    {
        float totalCritPower = (CritPower.GetValue() + Strength.GetValue()) * 0.01f; //�������� = �����������˺� + �����ӵ㣩* 1%
        //Debug.Log("total crit power " + totalCritPower);

        float critDamage = _damage * totalCritPower;  //�����˺� = �չ��˺� * ��������
        //Debug.Log("crit damage before round up " + critDamage);

        return Mathf.RoundToInt(critDamage);
    }

    #endregion


    public int GetMaxHp()  //��ȡ��ɫ�������ֵ��֮������UI
    {
        return MaxHp.GetValue() + Vitality.GetValue() * 5;
    }


    public Stats GetStat(StatType statType)
    {
        //PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        switch (statType)
        {
            case StatType.strength:
                return Strength;

            case StatType.agility:
                return Agility;

            case StatType.intelligence:
                return Intelligence;

            case StatType.vitality:
                return Vitality;

            case StatType.damage:
                return Damage;

            case StatType.critChance:
                return CritChance;

            case StatType.critPower:
                return CritPower;

            case StatType.health:
                return MaxHp;

            case StatType.armor:
                return Armor;

            case StatType.evasion:
                return Evasion;

            case StatType.magicalResistance:
                return MagicResistance;

            case StatType.fireDamage:
                return FireDamage;

            case StatType.iceDamage:
                return IceDamage;

            case StatType.lightingDamage:
                return LightingDamage;



            default:
                return null;

        }
    }
}
