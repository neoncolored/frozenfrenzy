using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts.UI
{
    public class AmmoBar : MonoBehaviour
    {
        public Slider ammoSlider;
        public GameObject ammoText;
        private float _maxAmmo = Player.Ammunition;
        private TextMeshProUGUI _ammoTextTMP;
        
        
        // Start is called before the first frame update
        void Start()
        {
            ammoSlider = ammoSlider.GetComponent<Slider>();
            ammoSlider.maxValue = _maxAmmo;
            ammoSlider.value = _maxAmmo;
            _ammoTextTMP = ammoText.GetComponent<TextMeshProUGUI>();
            _ammoTextTMP.SetText("15");
        }

        public void SetAmmo(float ammo)
        {
            ammoSlider.value = ammo;
            Player.Ammunition = (int)Math.Round(ammo);
            _ammoTextTMP.SetText((int)Math.Round(ammo) + "");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
    
