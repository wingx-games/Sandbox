using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        protected float maxHealth;
        private float _currentHealth;

        void Start()
        {
            _currentHealth = maxHealth;
        }

        public event Action<float> OnHealthChanged = delegate { };

        public void ModifyHealth(float amount)
        {
            _currentHealth += amount;
            float currentHealthPct = _currentHealth / maxHealth;
            OnHealthChanged((float)(_currentHealth / maxHealth));
        }
    }
}
