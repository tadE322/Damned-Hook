using JetBrains.Annotations;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    /// <summary>
    /// �� ������� ����� ����� ��� ������ ����� ������ � ������ SetHealth()
    /// </summary>
    [SerializeField] protected float health;
    /// <summary>
    /// �� ��� �������� ������ �� �������)
    /// </summary>
    [SerializeField] protected float maxHealth;

    /// <summary>
    /// ����������������� �������� ��������, ��� ����������� ��������� ��� �������� �� ������ � ��.
    /// ��� ����� �������� ������ ������
    /// </summary>
    public float Health => health;
    /// <summary>
    /// ������������ ������� ��������
    /// </summary>
    public float MaxHealth => maxHealth;

    /// <summary>
    /// ����������� �������� ��������. ���� �������� ����� 0 - ������, ���� >Max ����� - Max
    /// </summary>
    /// <param name="setHealth">�������� ��������</param>
    public virtual void SetHealth(float setHealth)
    {
        //������������� �������� ����� 0 � MaxHealth
        health = Mathf.Clamp(setHealth, 0, MaxHealth);
        if (health <= 0)
            Kill();
    }

    /// <summary>
    /// ������� ����
    /// </summary>
    /// <param name="damage">����</param>
    public virtual void ApplyDamage(float damage)
    {
        SetHealth(Health - damage);
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
