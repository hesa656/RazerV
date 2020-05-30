// Native UI Menu Template 3.0 - Abel Software
// You must download and use Scripthook V Dot Net Reference and NativeUI Reference (LINKS AT BOTTOM OF THE TEMPLATE)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;
using NativeUI;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Color = Corale.Colore.Core.Color;

public class NativeUITemplate : Script
{
    private Ped playerPed = Game.Player.Character;
    private Player player = Game.Player;
    private MenuPool _menuPool;

    Key[] healthKeys = { Key.Num8, Key.Num9, Key.Num4, Key.Num5, Key.Num6, Key.Num1, Key.Num2, Key.Num3 };

    public void PlayerModelMenu(UIMenu menu)
    {
        var playermodelmenu = _menuPool.AddSubMenu(menu, "Debug");
        for (int i = 0; i < 1; i++) ;

        var addHealth = new UIMenuItem("Add Health", "");
        var removeHealth = new UIMenuItem("-10 Health", "");
        var malecop = new UIMenuItem(playerPed.Health.ToString(), "");

        playermodelmenu.AddItem(addHealth);
        playermodelmenu.AddItem(removeHealth);
        playermodelmenu.AddItem(malecop);

        playermodelmenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == addHealth)
            {
                playerPed.Health = playerPed.MaxHealth;
                malecop.Text = playerPed.Health.ToString();
            }

            if (item == removeHealth)
            {
                playerPed.Health = playerPed.Health - 10;
                malecop.Text = playerPed.Health.ToString();
            }
        };
    }

    //Now we will add all of our sub menus into our main menu, and set the general information of the entire menu
    public NativeUITemplate()
    {
        playerPed.AlwaysDiesOnLowHealth = false;
        _menuPool = new MenuPool();
        var mainMenu = new UIMenu("~g~Razer ~w~V", "~w~Mod by TheWolf! ~r~V 1.0");
        _menuPool.Add(mainMenu);
        PlayerModelMenu(mainMenu); //Here we add the Player Model Sub Menu
        _menuPool.RefreshIndex();

        //This code will run with every ms tick
        Tick += OnTick;

        //This code will open the menu
        KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.F6 && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch
                mainMenu.Visible = !mainMenu.Visible;
        };

        var keyboard = Keyboard.Instance;

        Key[] otherPlayerKeys = { Key.A, Key.S, Key.D, Key.Space, Key.LeftShift, Key.V, Key.F };
        keyboard.SetKeys(Color.White, Key.W, otherPlayerKeys);
    }

    private void OnTick(object sender, EventArgs e)
    {
        _menuPool.ProcessMenus();
        var keyboard = Keyboard.Instance;

            //Check Player Health and change NumPad Colors
        #region
            if (Enumerable.Range(200, 171).Contains(playerPed.Health))
            {
                keyboard.SetKeys(Color.Green, Key.Num7, healthKeys);
            }
            else if (Enumerable.Range(170, 151).Contains(playerPed.Health))
            {
                keyboard.SetKeys(Color.Yellow, Key.Num7, healthKeys);
            }
            else if (Enumerable.Range(150, 131).Contains(playerPed.Health))
            {
                keyboard.SetKeys(Color.Orange, Key.Num7, healthKeys);
            }
            else if (Enumerable.Range(130, 100).Contains(playerPed.Health))
            {
                keyboard.SetKeys(Color.Red, Key.Num7, healthKeys);
            }
            #endregion

            if (player.WantedLevel == 0)
            {
                keyboard.SetKey(Key.D1, Corale.Colore.Core.Color.Black, false);
                keyboard.SetKey(Key.D2, Color.Black, false);
                keyboard.SetKey(Key.D3, Color.Black, false);
                keyboard.SetKey(Key.D4, Color.Black, false);
                keyboard.SetKey(Key.D5, Color.Black, false);
            }
            else if (player.WantedLevel == 1)
            {
                keyboard.SetKey(Key.D1, Color.Red, false);
            }
            else if (player.WantedLevel == 2)
            {
                keyboard.SetKey(Key.D1, Color.Red, false);
                keyboard.SetKey(Key.D2, Color.Blue, false);
            }
            else if (player.WantedLevel == 3)
            {
                keyboard.SetKey(Key.D1, Color.Red, false);
                keyboard.SetKey(Key.D2, Color.Blue, false);
                keyboard.SetKey(Key.D3, Color.Red, false);
            }
            else if (player.WantedLevel == 4)
            {
                keyboard.SetKey(Key.D1, Color.Red, false);
                keyboard.SetKey(Key.D2, Color.Blue, false);
                keyboard.SetKey(Key.D3, Color.Red, false);
                keyboard.SetKey(Key.D4, Color.Blue, false);
            }
            else if (player.WantedLevel == 5)
            {
                keyboard.SetKey(Key.D1, Color.Red, false);
                keyboard.SetKey(Key.D2, Color.Blue, false);
                keyboard.SetKey(Key.D3, Color.Red, false);
                keyboard.SetKey(Key.D4, Color.Blue, false);
                keyboard.SetKey(Key.D5, Color.Red, false);
            }

            if (player.IsAlive == false)
            {

            }

            if (player.IsAiming)
            {
                Key[] otherPlayerKeys = { Key.Space, Key.V, Key.F };
                keyboard.SetKeys(Color.Black, Key.LeftShift, otherPlayerKeys);
            }
            else
            {
                Key[] otherPlayerKeys = { Key.A, Key.S, Key.D, Key.Space, Key.LeftShift, Key.V, Key.F };
                keyboard.SetKeys(Color.White, Key.W, otherPlayerKeys);
            }
    }
}