using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using System.Drawing;


namespace ElKalista
{

    public class ElKalistaMenu
    {
        public static Menu _menu;

        public static void Initialize()
        {
            _menu = new Menu("ElKalista 复仇之矛", "menu", true);

            //ElKalista.Orbwalker
            var orbwalkerMenu = new Menu("走砍", "orbwalker");
            Kalista.Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);
            _menu.AddSubMenu(orbwalkerMenu);

            //ElKalista.TargetSelector
            var targetSelector = new Menu("目标选择", "TargetSelector");
            TargetSelector.AddToMenu(targetSelector);
            _menu.AddSubMenu(targetSelector);

            var cMenu = new Menu("一键连招", "Combo");
            cMenu.AddItem(new MenuItem("ElKalista.Combo.Q", "使用Q").SetValue(true));
            cMenu.AddItem(new MenuItem("ElKalista.Combo.E", "使用E").SetValue(true));
            cMenu.AddItem(new MenuItem("ElKalista.Combo.R", "使用R").SetValue(true));
            cMenu.AddItem(new MenuItem("ElKalista.sssssssss", ""));
            cMenu.AddItem(new MenuItem("ElKalista.ComboE.Auto", "叠加E直到击杀目标").SetValue(true));
            cMenu.AddItem(new MenuItem("ElKalista.ssssddsdssssss", ""));

            cMenu.AddItem(new MenuItem("ElKalista.hitChance", "Q准确度").SetValue(new StringList(new[] { "低", "中等", "高", "瞬间爆炸" }, 3)));
            cMenu.AddItem(new MenuItem("ElKalista.SemiR", "半手动施放R").SetValue(new KeyBind("T".ToCharArray()[0], KeyBindType.Press)));
            cMenu.AddItem(new MenuItem("ComboActive", "连招").SetValue(new KeyBind(32, KeyBindType.Press)));
            _menu.AddSubMenu(cMenu);

            var hMenu = new Menu("骚扰", "Harass");
            hMenu.AddItem(new MenuItem("ElKalista.Harass.Q", "使用Q").SetValue(true));
            hMenu.AddItem(new MenuItem("ElKalista.minmanaharass", "法力值控制")).SetValue(new Slider(60));
            hMenu.AddItem(new MenuItem("ElKalista.hitChance", "Q准确度").SetValue(new StringList(new[] { "低", "中等", "高", "瞬间爆炸" }, 3)));

            hMenu.SubMenu("自动骚扰敌方英雄").AddItem(new MenuItem("ElKalista.AutoHarass", "自动消耗开关", false).SetValue(new KeyBind("U".ToCharArray()[0], KeyBindType.Toggle)));
            hMenu.SubMenu("自动骚扰敌方英雄").AddItem(new MenuItem("ElKalista.UseQAutoHarass", "使用Q").SetValue(true));
            hMenu.SubMenu("自动骚扰敌方英雄").AddItem(new MenuItem("ElKalista.harass.mana", "法力值控制")).SetValue(new Slider(60));

            _menu.AddSubMenu(hMenu);

            var lMenu = new Menu("清线", "Clear");
            lMenu.AddItem(new MenuItem("useQFarm", "使用Q").SetValue(true));
            lMenu.AddItem(new MenuItem("ElKalista.Count.Minions", "当能击杀(_)个目标时使用Q >=").SetValue(new Slider(2, 1, 5)));
            lMenu.AddItem(new MenuItem("useEFarm", "使用E").SetValue(true));
            lMenu.AddItem(new MenuItem("ElKalista.Count.Minions.E", "当能击杀(_)个目标时使用E >=").SetValue(new Slider(2, 1, 5)));
            lMenu.AddItem(new MenuItem("useEFarmddsddaadsd", ""));
            lMenu.AddItem(new MenuItem("useQFarmJungle", "使用Q清野").SetValue(true));
            lMenu.AddItem(new MenuItem("useEFarmJungle", "使用E清野").SetValue(true));
            lMenu.AddItem(new MenuItem("useEFarmddssd", ""));
            lMenu.AddItem(new MenuItem("minmanaclear", "法力值控制")).SetValue(new Slider(60));

            _menu.AddSubMenu(lMenu);


            var itemMenu = new Menu("装备", "Items");
            itemMenu.AddItem(new MenuItem("ElKalista.Items.Youmuu", "使用幽梦之灵").SetValue(true));
            itemMenu.AddItem(new MenuItem("ElKalista.Items.Cutlass", "使用比尔吉沃特弯刀").SetValue(true));
            itemMenu.AddItem(new MenuItem("ElKalista.Items.Blade", "使用破败王者之刃").SetValue(true));
            itemMenu.AddItem(new MenuItem("ElKalista.Harasssfsddass.E", ""));
            itemMenu.AddItem(new MenuItem("ElKalista.Items.Blade.EnemyEHP", "敌方HP百分比(_)时使用").SetValue(new Slider(80, 100, 0)));
            itemMenu.AddItem(new MenuItem("ElKalista.Items.Blade.EnemyMHP", "我的HP百分比(_)时使用").SetValue(new Slider(80, 100, 0)));
            _menu.AddSubMenu(itemMenu);


            var setMenu = new Menu("其他选项", "SSS");
            setMenu.AddItem(new MenuItem("ElKalista.misc.save", "自动对契约者使用R").SetValue(true));
            setMenu.AddItem(new MenuItem("ElKalista.misc.allyhp", "当契约者生命少于(_)%自动使用R").SetValue(new Slider(25, 100, 0)));
            setMenu.AddItem(new MenuItem("useEFarmddsddsasfsasdsdsaadsd", ""));
            setMenu.AddItem(new MenuItem("ElKalista.E.Auto", "自动使用E").SetValue(true));
            setMenu.AddItem(new MenuItem("ElKalista.E.Stacks", "自动引爆E的层数").SetValue(new Slider(10, 1, 20)));
            setMenu.AddItem(new MenuItem("useEFafsdsgdrmddsddsasfsasdsdsaadsd", ""));
            setMenu.AddItem(new MenuItem("ElKalista.misc.ks", "连招模式下使用该设置").SetValue(false));
            setMenu.AddItem(new MenuItem("ElKalista.misc.junglesteal", "清野模式下使用该设置").SetValue(true));

            _menu.AddSubMenu(setMenu);

            //ElKalista.Misc
            var miscMenu = new Menu("范围指示器", "Misc");
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.off", "关闭所有范围指示器").SetValue(false));
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.Q", "Q 范围").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.W", "W 范围").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.E", "E 范围").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.R", "R 范围").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("ElKalista.Draw.Text", "指示文本").SetValue(true));

            var dmgAfterE = new MenuItem("ElKalista.DrawComboDamage", "E叠加伤害预算").SetValue(true);
            var drawFill = new MenuItem("ElKalista.DrawColour", "E伤害预算颜色", true).SetValue(new Circle(true, Color.FromArgb(204, 204, 0, 0)));
            miscMenu.AddItem(drawFill);
            miscMenu.AddItem(dmgAfterE);



            EDamage.DamageToUnit = Kalista.GetComboDamage;
            EDamage.Enabled = dmgAfterE.GetValue<bool>();
            EDamage.Fill = drawFill.GetValue<Circle>().Active;
            EDamage.FillColor = drawFill.GetValue<Circle>().Color;

            dmgAfterE.ValueChanged += delegate (object sender, OnValueChangeEventArgs eventArgs)
            {
                EDamage.Enabled = eventArgs.GetNewValue<bool>();
            };

            drawFill.ValueChanged += delegate (object sender, OnValueChangeEventArgs eventArgs)
            {
                EDamage.Fill = eventArgs.GetNewValue<Circle>().Active;
                EDamage.FillColor = eventArgs.GetNewValue<Circle>().Color;
            };

            _menu.AddSubMenu(miscMenu);

            //Here comes the moneyyy, money, money, moneyyyy
            var credits = new Menu("雨中的平野绫汉化", "jQuery");
            credits.AddItem(new MenuItem("ElKalista.Paypal", "你可以通过paypal捐赠原作者:info@zavox.nl"));
			credits.AddItem(new MenuItem("ElKalista.1", "更多最新汉化脚本加QQ群:273234870"));
			credits.AddItem(new MenuItem("ElKalista.2", "汉化更新日期:2015年3月24日"));
            _menu.AddSubMenu(credits);

            _menu.AddItem(new MenuItem("422442fsaafs4242f", ""));
            _menu.AddItem(new MenuItem("422442fsaafsf", "当前版本: 1.0.1.4"));
            _menu.AddItem(new MenuItem("fsasfafsfsafsa", "作者:jQuery"));

            _menu.AddToMainMenu();

            Console.WriteLine("Menu Loaded");
        }
    }
}