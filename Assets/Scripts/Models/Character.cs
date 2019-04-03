using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Character : MonoBehaviour
    {
        protected int _damage { get; set; }
        protected int _minDamage { get; set; }
        protected int _maxDamage { get; set; }
    } 
}
