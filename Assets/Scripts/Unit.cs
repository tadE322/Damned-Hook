using JetBrains.Annotations;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    /// <summary>
    /// Не трогать нигде кроме как внутри этого класса в методе SetHealth()
    /// </summary>
    [SerializeField] protected float health;
    /// <summary>
    /// Ну это наверное вообще не трогать)
    /// </summary>
    [SerializeField] protected float maxHealth;

    /// <summary>
    /// Инкапсулирвоанное значение здоровья, без возможности изменения без проверки на смерть и пр.
    /// Это будут смотреть другие классы
    /// </summary>
    public float Health => health;
    /// <summary>
    /// Максимальный уровень здоровья
    /// </summary>
    public float MaxHealth => maxHealth;

    /// <summary>
    /// Устрановить значение здоровья. Если здоровье будет 0 - смэрть, если >Max тогда - Max
    /// </summary>
    /// <param name="setHealth">Значение здоровья</param>
    public virtual void SetHealth(float setHealth)
    {
        //устанавливает значение между 0 и MaxHealth
        health = Mathf.Clamp(setHealth, 0, MaxHealth);
        if (health <= 0)
            Kill();
    }

    /// <summary>
    /// Нанести урон
    /// </summary>
    /// <param name="damage">Урон</param>
    public virtual void ApplyDamage(float damage)
    {
        SetHealth(Health - damage);
    }

    /// <summary>
    /// Убить юнит
    /// </summary>
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
