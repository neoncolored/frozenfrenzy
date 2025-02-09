using System;
using PlayerScripts;
using PlayerScripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Waves;

public class UpgradeMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject upgradeMenuPanel;
    public Button healthUpgradeButton;
    public Button attackUpgradeButton;
    public Button speedUpgradeButton;

    [Header("Player References")]
    public Player player;

    private bool _isShowing = false;
    
    private SampleWave _sampleWave;
    public PlayerHealthBar healthBar;


    private void Awake()
    {
        _sampleWave = FindObjectOfType<SampleWave>();
    }

    private void Start()
    {
        upgradeMenuPanel.SetActive(false);
        healthUpgradeButton.onClick.AddListener(OnHealthUpgrade);
        attackUpgradeButton.onClick.AddListener(OnAttackUpgrade);
        speedUpgradeButton.onClick.AddListener(OnSpeedUpgrade);
    }

    // -------------------------------------------
    // Wird vom Waves-System aufgerufen, wenn 
    // eine Wave beendet wurde.
    // -------------------------------------------
    public void ShowMenu()
    {
        upgradeMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        _isShowing = true;
    }

    // -------------------------------------------
    // Upgrade-Methoden für jeden Button
    // -------------------------------------------
    private void OnHealthUpgrade()
    {
        Player.MaxHp += 10;
        healthBar.SetHealth(Player.MaxHp);
        Player.Hp = Player.MaxHp;
        if (_isShowing) CloseMenu();
    }

    private void OnAttackUpgrade()
    {
        Projectile.Damage += 1;
        if (_isShowing) CloseMenu();
    }

    private void OnSpeedUpgrade()
    {
        player.speed += 2f;
        if (_isShowing) CloseMenu();
    }

    // -------------------------------------------
    // Schließt das Menü und setzt das Spiel fort
    // -------------------------------------------
    private void CloseMenu()
    {
        upgradeMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        _isShowing = false;

        _sampleWave.SetWave();
        _sampleWave.StartCoroutine(_sampleWave.StartWave());
    }
}
