using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;

namespace Program1
{
    enum ItemList
    {
        Trainee_Armor = 1,
        Iron_Armor,
        Sparta_Armor,
        Old_Sword,
        Bronze_Ax,
        Sparta_Spear,
        Sparta_Helmet,
    }

    internal class Program
    {
        static bool isPlay = true;

        static Player player;
        static Item item;

        static Item[] iteminfo = new Item[(int)ItemList.Sparta_Helmet + 1];

        private static void Main(string[] args)
        {
            Console.WriteLine("직업을 고르세요.");
            Console.WriteLine("1. 전사\t2. 전사\t3. 전사");
            string job = Console.ReadLine();

            Console.WriteLine("어디선가 스파르타의 기운이 느껴집니다...");

            player = new Player(1, "전사", 10, 5, 100, 6000);

            Thread.Sleep(500);

            while (isPlay)
            {
                ChoiceMenu();
            }
        }

        struct Player(int InLv, string InJob, int InDamage, int InDefense, int InHP, int InCoin)
        {
            public int level = InLv;
            public string ched = InJob;
            public int damage = InDamage;
            public int defense = InDefense;
            public int heart = InHP;
            public int coin = InCoin;
            public int weapon;
            public int shild;
        }

        private static void ChoiceMenu()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 휴식하기\n5. 종료하기");

            switch (ChoiceNumber())
            {
                case 1:
                    State();
                    break;
                case 2:
                    Inventory();
                    break;
                case 3:
                    Store();
                    break;
                case 4:
                    Rest();
                    break;
                case 5:
                    isPlay = false;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                    Thread.Sleep(500);
                    break;
            }
        }

        private static int ChoiceNumber()
        {
            Console.Write("원하시는 행동을 입력해주세요.\n>>");
            int outNumber = int.Parse(Console.ReadLine());

            return outNumber;
        }

        private static void Rest()
        {
            Console.Clear();

            int cost = 500;

            Console.WriteLine("휴식하기");
            Console.WriteLine($"{cost} G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.coin} G).\n");
            Console.WriteLine("0. 나가기\n0. 휴식하기");

            int choice = ChoiceNumber();

            if (choice == 0)
                ChoiceMenu();
            else if (choice == 1)
            {
                if (player.coin < cost)
                {
                    Console.WriteLine("Gold가 부족합니다.");

                    Thread.Sleep(500);

                    ChoiceMenu();
                }
                else
                {
                    Console.WriteLine("휴식을 완료했습니다.");

                    player.heart += 100;
                    player.coin -= cost;

                    Thread.Sleep(500);

                    Rest();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                Rest();
            }
        }

        private static void State()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            StatUpdate();

            Console.WriteLine("\n");

            Console.WriteLine("0. 나가기\n");

            if(ChoiceNumber() == 0)
                ChoiceMenu();
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                State();
            }
        }

        private static void StatUpdate()
        {
            Console.WriteLine("Lv. {0}", player.level);
            Console.WriteLine("Ched ( {0} )", player.ched);
            if (player.weapon != 0)
                Console.WriteLine("공격력 : {0}({1})", player.damage + player.weapon, player.weapon);
            else
                Console.WriteLine("공격력 : {0}", player.damage);
            if (player.shild != 0)
                Console.WriteLine("방어력 : {0}({1})", player.defense + player.shild, player.shild);
            else
                Console.WriteLine("방어력 : {0}", player.defense);
            Console.WriteLine("체력 : {0}", player.heart);
            Console.WriteLine("Gold : {0} G", player.coin);
        }

        private static void Inventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");

            int ownerNumber = 0;

            for (int i = 1; i < iteminfo.Length; i++)
            {
                if (iteminfo[i].isOwner)
                {
                    ownerNumber++;

                    if (iteminfo[i].isUse)
                    {
                        if (iteminfo[i].isShile)
                            Console.WriteLine("- {0} [E]{1}\t| 방어력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                        else if (iteminfo[i].isWeapon)
                            Console.WriteLine("- {0} [E]{1}\t| 공격력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                    }
                    else
                    {
                        if (iteminfo[i].isShile)
                            Console.WriteLine("- {0} {1}\t| 방어력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                        else if(iteminfo[i].isWeapon)
                            Console.WriteLine("- {0} {1}\t| 공격력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                    }
                }
            }

            Console.WriteLine("\n");

            Console.WriteLine("0. 나가기\n1. 장착 관리\n");

            int choice = ChoiceNumber();

            if (choice == 0)
                ChoiceMenu();
            else if (choice == 01)
                ItemUse();
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                Inventory();
            }
        }

        private static void ItemUse()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");

            int ownerNumber = 1;
            List<int> ownerNumberList = new List<int>();
            ownerNumberList.Add(0);

            for (int i = 1; i < iteminfo.Length; i++)
            {
                if (iteminfo[i].isOwner)
                {
                    ownerNumberList.Add(i);

                    if (iteminfo[i].isUse)
                    {
                        if (iteminfo[i].isShile)
                            Console.WriteLine("- {0} [E]{1}\t| 방어력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                        else if(iteminfo[i].isWeapon)
                            Console.WriteLine("- {0} [E]{1}\t| 공격력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);

                    }
                    else
                    {
                        if (iteminfo[i].isShile)
                            Console.WriteLine("- {0} {1}\t| 방어력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                        else if(iteminfo[i].isWeapon)
                            Console.WriteLine("- {0} {1}\t| 공격력 +{2}\t| {3}", ownerNumber, iteminfo[i].name,
                                iteminfo[i].value, iteminfo[i].information);
                    }

                    ownerNumber++;
                }
            }

            Console.WriteLine("\n");

            Console.WriteLine("0. 나가기\n");

            int choice = ChoiceNumber();

            if (choice == 0)
                Inventory();
            else if (choice <= ownerNumberList.Count)
            {
                if(iteminfo[ownerNumberList[choice]].isWeapon)
                {
                    for(int i = 1;  i < iteminfo.Length; i++)
                    {
                        if (iteminfo[i].isWeapon)
                            iteminfo[i].isUse = false;
                    }
                    player.weapon = iteminfo[ownerNumberList[choice]].value;
                }
                else if(iteminfo[ownerNumberList[choice]].isShile)
                {
                    for (int i = 1; i < iteminfo.Length; i++)
                    {
                        if (iteminfo[i].isShile)
                            iteminfo[i].isUse = false;
                    }
                    player.shild = iteminfo[ownerNumberList[choice]].value;
                }

                iteminfo[ownerNumberList[choice]].isUse = !iteminfo[ownerNumberList[choice]].isUse;

                ItemUse();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                ItemUse();
            }
        }

        private static void Store()
        {
            Console.Clear();

            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine("{0} G\n", player.coin);

            Console.WriteLine("[아이템 목록]");

            for (int i = 1; i < iteminfo.Length; i++)
            {
                ItemIndex(i);
            }

            for (int i = 1; i < iteminfo.Length; i++)
            {
                if (iteminfo[i].isOwner)
                {
                    if (iteminfo[i].isShile)
                        Console.WriteLine("- {0}\t| 방어력 +{1}\t| {2}\t| 구매완료", iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information);
                    else if(iteminfo[i].isWeapon)
                        Console.WriteLine("- {0}\t| 공격력 +{1}\t| {2}\t| 구매완료", iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information);

                }
                else
                {
                    if (iteminfo[i].isShile)
                        Console.WriteLine("- {0}\t| 방어력 +{1}\t| {2}\t| {3} G", iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, iteminfo[i].price);
                    else if(iteminfo[i].isWeapon)
                        Console.WriteLine("- {0}\t| 공격력 +{1}\t| {2}\t| {3} G", iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, iteminfo[i].price);
                }
            }

            Console.WriteLine("0. 나가기\n1. 아이템 구매\n2. 아이템 판매\n");
            
            int choice = ChoiceNumber();

            switch(choice)
            {
                case 0:
                    ChoiceMenu();
                    break;
                case 1:
                    ItemBuy();
                    break;
                case 2:
                    ItemSale();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                    Thread.Sleep(500);
                    Store();
                    break;
            }
        }

        private static void ItemBuy()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine("{0} G\n", player.coin);

            Console.WriteLine("[아이템 목록]");
            for (int i = 1; i < iteminfo.Length; i++)
            {
                if (iteminfo[i].isOwner)
                {
                    if (iteminfo[i].isShile)
                        Console.WriteLine("- {0} {1}\t| 방어력 +{2}\t| {3}\t| 구매완료", i, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information);
                    else if(iteminfo[i].isWeapon)
                        Console.WriteLine("- {0} {1}\t| 공격력 +{2}\t| {3}\t| 구매완료", i, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information);
                }
                else
                {
                    if (iteminfo[i].isShile)
                        Console.WriteLine("- {0} {1}\t| 방어력 +{2}\t| {3}\t| {4} G", i, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, iteminfo[i].price);
                    else if(iteminfo[i].isWeapon)
                        Console.WriteLine("- {0} {1}\t| 공격력 +{2}\t| {3}\t| {4} G", i, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, iteminfo[i].price);
                }
            }

            Console.WriteLine("0. 나가기\n");

            int choice = ChoiceNumber();

            if (choice == 0)
                Store();
            else if (choice <= iteminfo.Length)
            {
                if (iteminfo[choice].isOwner)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");

                    Thread.Sleep(500);
                    ItemBuy();
                }
                else
                {
                    if (player.coin >= iteminfo[choice].price)
                    {
                        Console.WriteLine("구매를 완료했습니다.");

                        player.coin -= iteminfo[choice].price;

                        iteminfo[choice].isOwner = true;

                        Thread.Sleep(500);
                        ItemBuy();
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");

                        Thread.Sleep(500);
                        ItemBuy();
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                Store();
            }
        }

        private static void ItemSale()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine("{0} G\n", player.coin);

            Console.WriteLine("[아이템 목록]");

            int ownerNumber = 1;
            List<int> ownerNumberList = new List<int>();
            ownerNumberList.Add(0);

            for (int i = 1; i < iteminfo.Length; i++)
            {
                if (iteminfo[i].isOwner)
                {
                    ownerNumberList.Add(i);

                    if (iteminfo[i].isShile)
                        Console.WriteLine("- {0} {1}\t| 방어력 +{2}\t| {3}\t| {4} G", ownerNumber, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, (int)(iteminfo[i].price * 0.85f));
                    else if (iteminfo[i].isWeapon)
                        Console.WriteLine("- {0} {1}\t| 공격력 +{2}\t| {3}\t| {4} G", ownerNumber, iteminfo[i].name,
                            iteminfo[i].value, iteminfo[i].information, (int)(iteminfo[i].price * 0.85f));

                    ownerNumber++;
                }
            }

            Console.WriteLine("0. 나가기\n");

            int choice = ChoiceNumber();

            if (choice == 0)
                Store();
            else if (choice <= ownerNumberList.Count)
            {
                if (iteminfo[ownerNumberList[choice]].isUse)
                {
                    if (iteminfo[ownerNumberList[choice]].isShile)
                        player.weapon = 0;
                    else if (iteminfo[ownerNumberList[choice]].isWeapon)
                        player.shild = 0;
                }

                player.coin += (int)(iteminfo[ownerNumberList[choice]].price * 0.85f);
                iteminfo[ownerNumberList[choice]].isUse = false;
                iteminfo[ownerNumberList[choice]].isOwner = false;


                Console.WriteLine("판매 완료. 장착 중이라면 자동으로 해제 됩니다.");
                Thread.Sleep(500);
                ItemSale();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 잠시만 기다리세요.");

                Thread.Sleep(500);
                Store();
            }
        }

        private static void ItemIndex(int InItemKey)
        {
            switch (InItemKey)
            {
                case (int)ItemList.Trainee_Armor:
                    iteminfo[InItemKey].name = "수련자의 갑옷";
                    iteminfo[InItemKey].value = 5;
                    iteminfo[InItemKey].price = 1000;
                    iteminfo[InItemKey].information = "수련에 도움을 주는 갑옷입니다.";
                    iteminfo[InItemKey].isShile = true;
                    break;
                case (int)ItemList.Iron_Armor:
                    iteminfo[InItemKey].name = "무쇠 갑옷";
                    iteminfo[InItemKey].value = 9;
                    iteminfo[InItemKey].price = 2000;
                    iteminfo[InItemKey].information = "무쇠로 만들어져 튼튼한 갑옷입니다.";
                    iteminfo[InItemKey].isShile = true;
                    break;
                case (int)ItemList.Sparta_Armor:
                    iteminfo[InItemKey].name = "스파르타의 갑옷";
                    iteminfo[InItemKey].value = 15;
                    iteminfo[InItemKey].price = 3500;
                    iteminfo[InItemKey].information = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
                    iteminfo[InItemKey].isShile = true;
                    break;
                case (int)ItemList.Old_Sword:
                    iteminfo[InItemKey].name = "낡은 검";
                    iteminfo[InItemKey].value = 2;
                    iteminfo[InItemKey].price = 600;
                    iteminfo[InItemKey].information = "쉽게 볼 수 있는 낡은 검 입니다.";
                    iteminfo[InItemKey].isWeapon = true;
                    break;
                case (int)ItemList.Bronze_Ax:
                    iteminfo[InItemKey].name = "청동 도끼";
                    iteminfo[InItemKey].value = 5;
                    iteminfo[InItemKey].price = 1500;
                    iteminfo[InItemKey].information = "어디선가 사용됐던거 같은 도끼입니다.";
                    iteminfo[InItemKey].isWeapon = true;
                    break;
                case (int)ItemList.Sparta_Spear:
                    iteminfo[InItemKey].name = "스파르타의 창";
                    iteminfo[InItemKey].value = 7;
                    iteminfo[InItemKey].price = 2100;
                    iteminfo[InItemKey].information = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
                    iteminfo[InItemKey].isWeapon = true;
                    break;
                case (int)ItemList.Sparta_Helmet:
                    iteminfo[InItemKey].name = "스파르타의 투구";
                    iteminfo[InItemKey].value = 10;
                    iteminfo[InItemKey].price = 2500;
                    iteminfo[InItemKey].information = "스파르타의 전사들이 사용했다는 전설의 투구입니다.";
                    iteminfo[InItemKey].isShile = true;
                    break;
            }
        }

        struct Item(string InName, string InInformation, int InPrice, int InValue)
        {
            public bool isUse = false;
            public bool isOwner = false;

            public bool isWeapon = false;
            public bool isShile = false;

            public string name = InName;
            public string information = InInformation;
            public int price = InPrice;
            public int value = InValue;
        }
    }
}