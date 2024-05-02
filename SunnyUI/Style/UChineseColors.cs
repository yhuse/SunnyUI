/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UChineseColors.cs
 * 文件说明: 中国传统颜色
 * 当前版本: V3.1
 * 创建日期: 2021-12-07
 *
 * 2021-12-07: V3.0.9 增加文件说明
 ******************************************************************************
 * 参考：https://colors.ichuantong.cn
 * 参考：http://blog.sina.com.cn/s/blog_5c3b139d0101deia.html
******************************************************************************/

using System.Drawing;

namespace Sunny.UI
{
    public static class ChineseColors
    {
        public static class 红色系
        {
            /// <summary>
            /// 粉红、浅红色：别称：妃色、杨妃色、湘妃色、妃红色
            /// </summary>
            public readonly static Color 粉红 = Color.FromArgb(255, 179, 167);

            /// <summary>
            /// 妃色、妃红色：古同"绯"，粉红色。杨妃色、湘妃色、粉红皆同义
            /// </summary>
            public readonly static Color 妃色 = Color.FromArgb(237, 87, 54);

            /// <summary>
            /// 品红：比大红浅的红色
            ///  - 注：这里的"品红"估计是指的“一品红”，是基于大红色系的，和现在我们印刷用色的“品红M100”不是一个概念
            /// </summary>
            public readonly static Color 品红 = Color.FromArgb(240, 0, 86);

            /// <summary>
            /// 桃红：桃花的颜色，比粉红略鲜润的颜色
            ///  - 注：不大于M70的色彩，有时可加入适量黄色 
            /// </summary>
            public readonly static Color 桃红 = Color.FromArgb(244, 121, 131);

            /// <summary>
            /// 海棠红：淡紫红色、较桃红色深一些，是非常妩媚娇艳的颜色
            /// </summary>
            public readonly static Color 海棠红 = Color.FromArgb(219, 90, 107);

            /// <summary>
            /// 石榴红：石榴花的颜色，高色度和纯度的红色
            /// </summary>
            public readonly static Color 石榴红 = Color.FromArgb(242, 12, 0);

            /// <summary>
            /// 樱桃色：鲜红色
            /// </summary>
            public readonly static Color 樱桃色 = Color.FromArgb(201, 55, 86);

            /// <summary>
            /// 银红：银朱和粉红色颜料配成的颜色。多用来形容有光泽的各种红色，尤指有光泽浅红
            /// </summary>
            public readonly static Color 银红 = Color.FromArgb(240, 86, 84);

            /// <summary>
            /// 大红：正红色，三原色中的红，传统的中国红，又称绛色
            ///  - 注：RGB 色中的 R255 系列明度
            /// </summary>
            public readonly static Color 大红 = Color.FromArgb(255, 33, 33);

            /// <summary>
            /// 绛紫：紫中略带红的颜色
            /// </summary>
            public readonly static Color 绛紫 = Color.FromArgb(140, 67, 86);

            /// <summary>
            /// 绯红：艳丽的深红
            /// </summary>
            public readonly static Color 绯红 = Color.FromArgb(200, 60, 35);

            /// <summary>
            /// 胭脂：
            /// 1，女子装扮时用的胭脂的颜色；
            /// 2，国画暗红色颜料 
            /// </summary>
            public readonly static Color 胭脂 = Color.FromArgb(157, 41, 51);

            /// <summary>
            /// 朱红：朱砂的颜色，比大红活泼，也称铅朱、朱色、丹色
            ///  - 注：在YM对等的情况下，适量减少红色的成分就是该色的色彩系列感觉
            /// </summary>
            public readonly static Color 朱红 = Color.FromArgb(255, 76, 0);

            /// <summary>
            /// 丹：丹砂的鲜艳红色
            /// </summary>
            public readonly static Color 丹 = Color.FromArgb(255, 78, 32);

            /// <summary>
            /// 彤：赤色
            /// </summary>
            public readonly static Color 彤 = Color.FromArgb(243, 83, 54);

            /// <summary>
            /// 茜色：茜草染的色彩，呈深红色
            /// </summary>
            public readonly static Color 茜色 = Color.FromArgb(203, 58, 86);

            /// <summary>
            /// 火红：火焰的红色，赤色
            /// </summary>
            public readonly static Color 火红 = Color.FromArgb(255, 45, 81);

            /// <summary>
            /// 赫赤：深红，火红。泛指赤色、火红色
            /// </summary>
            public readonly static Color 赫赤 = Color.FromArgb(201, 31, 55);

            /// <summary>
            /// 嫣红：鲜艳的红色
            /// </summary>
            public readonly static Color 嫣红 = Color.FromArgb(239, 122, 130);

            /// <summary>
            /// 洋红：色橘红
            ///  - 注：这个色彩方向不太对，通常洋红指的是倾向于M100系列的红色，应该削弱黄色成分
            /// </summary>
            public readonly static Color 洋红 = Color.FromArgb(255, 0, 151);

            /// <summary>
            /// 炎：引申为红色
            /// </summary>
            public readonly static Color 炎 = Color.FromArgb(255, 51, 0);

            /// <summary>
            /// 赤：本义火的颜色，即红色
            /// </summary>
            public readonly static Color 赤 = Color.FromArgb(195, 39, 43);

            /// <summary>
            /// 绾：绛色；浅绛色
            /// </summary>
            public readonly static Color 绾 = Color.FromArgb(169, 129, 117);

            /// <summary>
            /// 枣红：即深红
            ///  - 注：色相不变，是深浅变化
            /// </summary>
            public readonly static Color 枣红 = Color.FromArgb(195, 33, 54);

            /// <summary>
            /// 檀：浅红色，浅绛色
            /// </summary>
            public readonly static Color 檀 = Color.FromArgb(179, 109, 97);

            /// <summary>
            /// 殷红：发黑的红色
            /// </summary>
            public readonly static Color 殷红 = Color.FromArgb(190, 0, 47);

            /// <summary>
            /// 酡红：像饮酒后脸上泛现的红色，泛指脸红
            /// </summary>
            public readonly static Color 酡红 = Color.FromArgb(220, 48, 35);

            /// <summary>
            /// 酡颜：饮酒脸红的样子。亦泛指脸红色
            /// </summary>
            public readonly static Color 酡颜 = Color.FromArgb(249, 144, 111);
        }

        public static class 黄色系
        {
            /// <summary>
            /// 鹅黄：淡黄色
            ///  - 注：鹅嘴的颜色，高明度微偏红黄色
            /// </summary>
            public readonly static Color 鹅黄 = Color.FromArgb(255, 241, 67);

            /// <summary>
            /// 鸭黄：小鸭毛的黄色
            /// </summary>
            public readonly static Color 鸭黄 = Color.FromArgb(250, 255, 114);

            /// <summary>
            /// 樱草色：淡黄色
            /// </summary>
            public readonly static Color 樱草色 = Color.FromArgb(234, 255, 86);

            /// <summary>
            /// 杏黄：成熟杏子的黄色
            ///  - 注：Y100 M20~30 感觉的色彩，比较常用且有浓郁中国味道
            /// </summary>
            public readonly static Color 杏黄 = Color.FromArgb(255, 166, 49);

            /// <summary>
            /// 杏红：成熟杏子偏红色的一种颜色
            /// </summary>
            public readonly static Color 杏红 = Color.FromArgb(255, 140, 49);

            /// <summary>
            /// 橘黄：柑橘的黄色
            /// </summary>
            public readonly static Color 橘黄 = Color.FromArgb(255, 137, 54);

            /// <summary>
            /// 橙黄：橙子的黄色 
            ///  - 注：Y100 M50 感觉的色彩，现代感比较强。广告上用得较多 
            /// </summary>
            public readonly static Color 橙黄 = Color.FromArgb(255, 164, 0);

            /// <summary>
            /// 橘红：柑橘皮所呈现的红色
            /// </summary>
            public readonly static Color 橘红 = Color.FromArgb(255, 117, 0);

            /// <summary>
            /// 姜黄：中药名。别名黄姜。为姜科植物姜黄的根茎。又指人脸色不正，呈黄白色
            /// </summary>
            public readonly static Color 姜黄 = Color.FromArgb(255, 199, 115);

            /// <summary>
            /// 缃色：浅黄色
            /// </summary>
            public readonly static Color 缃色 = Color.FromArgb(240, 194, 57);

            /// <summary>
            /// 橙色：界于红色和黄色之间的混合色
            /// </summary>
            public readonly static Color 橙色 = Color.FromArgb(250, 140, 53);

            /// <summary>
            /// 茶色：一种比栗色稍红的棕橙色至浅棕色
            /// </summary>
            public readonly static Color 茶色 = Color.FromArgb(179, 92, 68);

            /// <summary>
            /// 驼色：一种比咔叽色稍红而微淡、比肉桂色黄而稍淡和比核桃棕色黄而暗的浅黄棕色
            /// </summary>
            public readonly static Color 驼色 = Color.FromArgb(168, 132, 98);

            /// <summary>
            /// 昏黄：形容天色、灯光等呈幽暗的黄色
            /// </summary>
            public readonly static Color 昏黄 = Color.FromArgb(200, 155, 64);

            /// <summary>
            /// 栗色：栗壳的颜色。即紫黑色
            /// </summary>
            public readonly static Color 栗色 = Color.FromArgb(96, 40, 30);

            /// <summary>
            /// 棕色：棕毛的颜色，即褐色 
            /// 1，在红色和黄色之间的任何一种颜色；
            /// 2，适中的暗淡和适度的浅黑 
            /// </summary>
            public readonly static Color 棕色 = Color.FromArgb(178, 93, 37);

            /// <summary>
            /// 棕绿：绿中泛棕色的一种颜色 
            /// </summary>
            public readonly static Color 棕绿 = Color.FromArgb(130, 113, 0);

            /// <summary>
            /// 棕黑：深棕色 
            /// </summary>
            public readonly static Color 棕黑 = Color.FromArgb(124, 75, 0);

            /// <summary>
            /// 棕红：红褐色 
            /// </summary>
            public readonly static Color 棕红 = Color.FromArgb(155, 68, 0);

            /// <summary>
            /// 棕黄：浅褐色 
            /// </summary>
            public readonly static Color 棕黄 = Color.FromArgb(174, 112, 0);

            /// <summary>
            /// 赭：赤红如赭土的颜料，古人用以饰面 
            /// </summary>
            public readonly static Color 赭 = Color.FromArgb(156, 83, 51);

            /// <summary>
            /// 赭色：红色、赤红色 
            /// </summary>
            public readonly static Color 赭色 = Color.FromArgb(149, 85, 57);

            /// <summary>
            /// 琥珀：琥珀色 
            /// </summary>
            public readonly static Color 琥珀 = Color.FromArgb(202, 105, 36);

            /// <summary>
            /// 褐色：黄黑色 
            /// </summary>
            public readonly static Color 褐色 = Color.FromArgb(110, 81, 30);

            /// <summary>
            /// 枯黄：干枯焦黄 
            /// </summary>
            public readonly static Color 枯黄 = Color.FromArgb(211, 177, 125);

            /// <summary>
            /// 黄栌：一种落叶灌木，花黄绿色，叶子秋天变成红色。木材黄色可做染料 
            /// </summary>
            public readonly static Color 黄栌 = Color.FromArgb(226, 156, 69);

            /// <summary>
            /// 秋色：
            /// 1，中常橄榄棕色,它比一般橄榄棕色稍暗,且稍稍绿些；
            /// 2，古以秋为金,其色白,故代指白色 
            /// </summary>
            public readonly static Color 秋色 = Color.FromArgb(137, 108, 57);

            /// <summary>
            /// 秋香色：浅橄榄色 浅黄绿色
            ///  - 注：直接在Y中掺入k10~30可得到不同浓淡的该类色彩 
            /// </summary>
            public readonly static Color 秋香色 = Color.FromArgb(217, 182, 17);
        }

        public static class 绿色系
        {
            /// <summary>
            /// 嫩绿：像刚长出的嫩叶的浅绿色
            /// </summary>
            public readonly static Color 嫩绿 = Color.FromArgb(189, 221, 34);

            /// <summary>
            /// 柳黄：像柳树芽那样的浅黄色
            /// </summary>
            public readonly static Color 柳黄 = Color.FromArgb(201, 221, 34);

            /// <summary>
            /// 柳绿：柳叶的青绿色
            /// </summary>
            public readonly static Color 柳绿 = Color.FromArgb(175, 221, 34);

            /// <summary>
            /// 竹青：竹子的绿色
            /// </summary>
            public readonly static Color 竹青 = Color.FromArgb(120, 146, 98);

            /// <summary>
            /// 葱黄：黄绿色，嫩黄色
            /// </summary>
            public readonly static Color 葱黄 = Color.FromArgb(163, 217, 0);

            /// <summary>
            /// 葱绿：
            /// 1，浅绿又略显微黄的颜色；
            /// 2，草木青翠的样子
            /// </summary>
            public readonly static Color 葱绿 = Color.FromArgb(158, 217, 0);

            /// <summary>
            /// 葱青：淡淡的青绿色
            /// </summary>
            public readonly static Color 葱青 = Color.FromArgb(14, 184, 58);

            /// <summary>
            /// 葱倩：青绿色
            /// </summary>
            public readonly static Color 葱倩 = Color.FromArgb(14, 184, 58);

            /// <summary>
            /// 青葱：翠绿色,形容植物浓绿
            /// </summary>
            public readonly static Color 青葱 = Color.FromArgb(10, 163, 68);

            /// <summary>
            /// 油绿：光润而浓绿的颜色。以上几种绿色都是明亮可爱的色彩
            /// </summary>
            public readonly static Color 油绿 = Color.FromArgb(0, 188, 18);

            /// <summary>
            /// 绿沈（沉）：深绿
            /// </summary>
            public readonly static Color 绿沈 = Color.FromArgb(12, 137, 24);

            /// <summary>
            /// 碧色：
            /// 1，青绿色；
            /// 2，青白色,浅蓝色
            /// </summary>
            public readonly static Color 碧色 = Color.FromArgb(27, 209, 165);

            /// <summary>
            /// 碧绿：鲜艳的青绿色
            /// </summary>
            public readonly static Color 碧绿 = Color.FromArgb(42, 221, 156);

            /// <summary>
            /// 青碧：鲜艳的青蓝色
            /// </summary>
            public readonly static Color 青碧 = Color.FromArgb(72, 192, 163);

            /// <summary>
            /// 翡翠色：
            /// 1，翡翠鸟羽毛的青绿色；
            /// 2，翡翠宝石的颜色
            ///  - 注：C-Y≥30 的系列色彩，多与白色配合以体现清新明丽感觉，与黑色配合效果不好，该色个性柔弱，会被黑色牵制
            /// </summary>
            public readonly static Color 翡翠色 = Color.FromArgb(61, 225, 173);

            /// <summary>
            /// 草绿：绿而略黄的颜色
            /// </summary>
            public readonly static Color 草绿 = Color.FromArgb(64, 222, 90);

            /// <summary>
            /// 青色：
            /// 1，一类带绿的蓝色,中等深浅,高度饱和；
            /// 2，特指三补色中的一色；
            /// 3，本义是蓝色；
            /// 4，一般指深绿色；
            /// 5，也指黑色；
            /// 6，四色印刷中的一色
            /// </summary>
            public readonly static Color 青色 = Color.FromArgb(0, 224, 158);

            /// <summary>
            /// 青翠：鲜绿
            /// </summary>
            public readonly static Color 青翠 = Color.FromArgb(0, 224, 121);

            /// <summary>
            /// 青白：白而发青,尤指脸没有血色
            /// </summary>
            public readonly static Color 青白 = Color.FromArgb(192, 235, 215);

            /// <summary>
            /// 鸭卵青：淡青灰色，极淡的青绿色
            /// </summary>
            public readonly static Color 鸭卵青 = Color.FromArgb(224, 238, 232);

            /// <summary>
            /// 蟹壳青：深灰绿色
            /// </summary>
            public readonly static Color 蟹壳青 = Color.FromArgb(187, 205, 197);

            /// <summary>
            /// 鸦青：鸦羽的颜色。即黑而带有紫绿光的颜色
            /// </summary>
            public readonly static Color 鸦青 = Color.FromArgb(66, 76, 80);

            /// <summary>
            /// 绿色：
            /// 1，在光谱中介于蓝与黄之间的那种颜色；
            /// 2，本义：青中带黄的颜色；
            /// 3，引申为黑色，如绿鬓：乌黑而光亮的鬓发。代指为青春年少的容颜
            ///  - 注：现代色彩研究中，把绿色提高到了一个重要的位置，和其它红黄兰三原色并列研究，称做"心理原色"之一
            /// </summary>
            public readonly static Color 绿色 = Color.FromArgb(0, 229, 0);

            /// <summary>
            /// 豆绿：浅黄绿色
            /// </summary>
            public readonly static Color 豆绿 = Color.FromArgb(158, 208, 72);

            /// <summary>
            /// 豆青：浅青绿色
            /// </summary>
            public readonly static Color 豆青 = Color.FromArgb(150, 206, 84);

            /// <summary>
            /// 石青：淡灰绿色
            /// </summary>
            public readonly static Color 石青 = Color.FromArgb(123, 207, 166);

            /// <summary>
            /// 玉色：玉的颜色，高雅的淡绿、淡青色
            /// </summary>
            public readonly static Color 玉色 = Color.FromArgb(46, 223, 163);

            /// <summary>
            /// 缥：绿色而微白
            /// </summary>
            public readonly static Color 缥 = Color.FromArgb(127, 236, 173);

            /// <summary>
            /// 艾绿：艾草的颜色。偏苍白的绿色
            /// </summary>
            public readonly static Color 艾绿 = Color.FromArgb(164, 226, 198);

            /// <summary>
            /// 松柏绿：经冬松柏叶的深绿
            /// </summary>
            public readonly static Color 松柏绿 = Color.FromArgb(33, 166, 117);

            /// <summary>
            /// 松花绿：亦作"松花"、"松绿"。偏黑的深绿色，墨绿
            /// </summary>
            public readonly static Color 松花绿 = Color.FromArgb(5, 119, 72);

            /// <summary>
            /// 松花色：浅黄绿色。松树花粉的颜色，《红楼梦》中提及松花配桃红为娇艳
            /// </summary>
            public readonly static Color 松花色 = Color.FromArgb(188, 230, 114);
        }

        public static class 蓝色系
        {
            /// <summary>
            /// 蓝：三原色的一种。像晴天天空的颜色
            ///  - 注：这里的蓝色指的不是RGB色彩中的B，而是CMY色彩中的C
            /// </summary>
            public readonly static Color 蓝 = Color.FromArgb(68, 206, 246);

            /// <summary>
            /// 靛青：也叫"蓝靛"。用蓼蓝叶泡水调和与石灰沉淀所得的蓝色染料。呈深蓝绿色
            ///  - 注：靛，发音dian四声，有些地方将蓝墨水称为"靛水"或者"兰靛水"
            /// </summary>
            public readonly static Color 靛青 = Color.FromArgb(23, 124, 176);

            /// <summary>
            /// 靛蓝：由植物(例如靛蓝或菘蓝属植物)得到的蓝色染料
            /// </summary>
            public readonly static Color 靛蓝 = Color.FromArgb(6, 82, 121);

            /// <summary>
            /// 碧蓝：青蓝色
            /// </summary>
            public readonly static Color 碧蓝 = Color.FromArgb(62, 237, 231);

            /// <summary>
            /// 蔚蓝：类似晴朗天空的颜色的一种蓝色
            /// </summary>
            public readonly static Color 蔚蓝 = Color.FromArgb(112, 243, 255);

            /// <summary>
            /// 宝蓝：鲜艳明亮的蓝色
            ///  - 注：英文中为 RoyalBlue 即皇家蓝色，是皇室选用的色彩，多和小面积纯黄色（金色）配合使用
            /// </summary>
            public readonly static Color 宝蓝 = Color.FromArgb(75, 92, 196);

            /// <summary>
            /// 蓝灰色：一种近于灰略带蓝的深灰色
            /// </summary>
            public readonly static Color 蓝灰色 = Color.FromArgb(161, 175, 201);

            /// <summary>
            /// 藏青：蓝而近黑
            /// </summary>
            public readonly static Color 藏青 = Color.FromArgb(46, 78, 126);

            /// <summary>
            /// 藏蓝：蓝里略透红色
            /// </summary>
            public readonly static Color 藏蓝 = Color.FromArgb(59, 46, 126);

            /// <summary>
            /// 黛：青黑色的颜料。古代女子用以画眉
            /// </summary>
            public readonly static Color 黛 = Color.FromArgb(74, 66, 102);

            /// <summary>
            /// 黛螺：绘画或画眉所使用的青黑色颜料，代指女子眉妩
            /// </summary>
            public readonly static Color 黛螺 = Color.FromArgb(74, 66, 102);

            /// <summary>
            /// 黛色：青黑色。
            /// </summary>
            public readonly static Color 黛色 = Color.FromArgb(74, 66, 102);

            /// <summary>
            /// 黛绿：墨绿。
            /// </summary>
            public readonly static Color 黛绿 = Color.FromArgb(66, 102, 102);

            /// <summary>
            /// 黛蓝：深蓝色
            /// </summary>
            public readonly static Color 黛蓝 = Color.FromArgb(66, 80, 102);

            /// <summary>
            /// 黛紫：深紫色
            /// </summary>
            public readonly static Color 黛紫 = Color.FromArgb(87, 66, 102);

            /// <summary>
            /// 紫色：蓝和红组成的颜色。古人以紫为祥瑞的颜色。代指与帝王、皇宫有关的事物
            /// </summary>
            public readonly static Color 紫色 = Color.FromArgb(141, 75, 187);

            /// <summary>
            /// 紫酱：浑浊的紫色
            /// </summary>
            public readonly static Color 紫酱 = Color.FromArgb(129, 84, 99);

            /// <summary>
            /// 酱紫：紫中略带红的颜色
            /// </summary>
            public readonly static Color 酱紫 = Color.FromArgb(129, 84, 118);

            /// <summary>
            /// 紫檀：檀木的颜色，也称乌檀色、乌木色
            /// </summary>
            public readonly static Color 紫檀 = Color.FromArgb(76, 34, 27);

            /// <summary>
            /// 绀青 绀紫：纯度较低的深紫色
            /// </summary>
            public readonly static Color 绀青 = Color.FromArgb(0, 51, 113);

            /// <summary>
            /// 紫棠：黑红色
            /// </summary>
            public readonly static Color 紫棠 = Color.FromArgb(86, 0, 79);

            /// <summary>
            /// 青莲：偏蓝的紫色
            /// </summary>
            public readonly static Color 青莲 = Color.FromArgb(128, 29, 174);

            /// <summary>
            /// 群青：深蓝色
            /// </summary>
            public readonly static Color 群青 = Color.FromArgb(76, 141, 174);

            /// <summary>
            /// 雪青：浅蓝紫色
            /// </summary>
            public readonly static Color 雪青 = Color.FromArgb(176, 164, 227);

            /// <summary>
            /// 丁香色：紫丁香的颜色，浅浅的紫色，很娇柔淡雅的色彩
            /// </summary>
            public readonly static Color 丁香色 = Color.FromArgb(204, 164, 227);

            /// <summary>
            /// 藕色：浅灰而略带红的颜色
            /// </summary>
            public readonly static Color 藕色 = Color.FromArgb(237, 209, 216);

            /// <summary>
            /// 藕荷色：浅紫而略带红的颜色
            /// </summary>
            public readonly static Color 藕荷色 = Color.FromArgb(228, 198, 208);
        }

        public static class 苍色系
        {
            /// <summary>
            /// 苍色：即各种颜色掺入黑色后的颜色
            /// </summary>
            public readonly static Color 苍色 = Color.FromArgb(117, 135, 138);

            /// <summary>
            /// 苍翠
            /// </summary>
            public readonly static Color 苍翠 = Color.FromArgb(81, 154, 115);

            /// <summary>
            /// 苍黄
            /// </summary>
            public readonly static Color 苍黄 = Color.FromArgb(162, 155, 124);

            /// <summary>
            /// 苍青
            /// </summary>
            public readonly static Color 苍青 = Color.FromArgb(115, 151, 171);

            /// <summary>
            /// 苍黑
            /// </summary>
            public readonly static Color 苍黑 = Color.FromArgb(57, 82, 96);

            /// <summary>
            /// 苍白
            ///  - 注：准确的说是掺入不同灰度级别的灰色
            /// </summary>
            public readonly static Color 苍白 = Color.FromArgb(209, 217, 224);
        }

        public static class 水色系
        {
            /// <summary>
            /// 水色
            /// </summary>
            public readonly static Color 水色 = Color.FromArgb(136, 173, 166);

            /// <summary>
            /// 水红
            /// </summary>
            public readonly static Color 水红 = Color.FromArgb(243, 211, 231);

            /// <summary>
            /// 水绿
            /// </summary>
            public readonly static Color 水绿 = Color.FromArgb(212, 242, 231);

            /// <summary>
            /// 水蓝
            /// </summary>
            public readonly static Color 水蓝 = Color.FromArgb(210, 240, 244);

            /// <summary>
            /// 淡青
            /// </summary>
            public readonly static Color 淡青 = Color.FromArgb(211, 224, 243);

            /// <summary>
            /// 湖蓝
            /// </summary>
            public readonly static Color 湖蓝 = Color.FromArgb(48, 223, 243);

            /// <summary>
            /// 湖绿，皆是浅色。深色淡色：颜色深的或浅的，不再一一列出
            /// </summary>
            public readonly static Color 湖绿 = Color.FromArgb(37, 248, 203);
        }

        public static class 灰白色系
        {
            /// <summary>
            /// 精白：纯白，洁白，净白，粉白
            /// </summary>
            public readonly static Color 精白 = Color.FromArgb(255, 255, 255);

            /// <summary>
            /// 象牙白：乳白色
            /// </summary>
            public readonly static Color 象牙白 = Color.FromArgb(255, 251, 240);

            /// <summary>
            /// 雪白：如雪般洁白
            /// </summary>
            public readonly static Color 雪白 = Color.FromArgb(240, 252, 255);

            /// <summary>
            /// 月白：淡蓝色
            /// </summary>
            public readonly static Color 月白 = Color.FromArgb(214, 236, 240);

            /// <summary>
            /// 缟：白色
            /// </summary>
            public readonly static Color 缟 = Color.FromArgb(242, 236, 222);

            /// <summary>
            /// 素：白色，无色
            /// </summary>
            public readonly static Color 素 = Color.FromArgb(224, 240, 233);

            /// <summary>
            /// 荼白：如荼之白色
            /// </summary>
            public readonly static Color 荼白 = Color.FromArgb(243, 249, 241);

            /// <summary>
            /// 霜色：白霜的颜色
            /// </summary>
            public readonly static Color 霜色 = Color.FromArgb(233, 241, 246);

            /// <summary>
            /// 花白：白色和黑色混杂的。斑白的 夹杂有灰色的白
            /// </summary>
            public readonly static Color 花白 = Color.FromArgb(194, 204, 208);

            /// <summary>
            /// 鱼肚白：似鱼腹部的颜色，多指黎明时东方的天色颜色
            ///  - 注：M5 Y5
            /// </summary>
            public readonly static Color 鱼肚白 = Color.FromArgb(252, 239, 232);

            /// <summary>
            /// 莹白：晶莹洁白
            /// </summary>
            public readonly static Color 莹白 = Color.FromArgb(227, 249, 253);

            /// <summary>
            /// 灰色：黑色和白色混和成的一种颜色
            /// </summary>
            public readonly static Color 灰色 = Color.FromArgb(128, 128, 128);

            /// <summary>
            /// 牙色：与象牙相似的淡黄色
            ///  - 注：暖白
            /// </summary>
            public readonly static Color 牙色 = Color.FromArgb(238, 222, 176);

            /// <summary>
            /// 铅白：铅粉的白色。铅粉，国画颜料，日久易氧化"返铅"变黑。铅粉在古时用以搽脸的化妆品
            ///  - 注：冷白
            /// </summary>
            public readonly static Color 铅白 = Color.FromArgb(240, 240, 244);
        }

        public static class 黑色系
        {
            /// <summary>
            /// 玄色：赤黑色，黑中带红的颜色，又泛指黑色
            /// </summary>
            public readonly static Color 玄色 = Color.FromArgb(98, 42, 29);

            /// <summary>
            /// 玄青：深黑色
            /// </summary>
            public readonly static Color 玄青 = Color.FromArgb(61, 59, 79);

            /// <summary>
            /// 乌色：暗而呈黑的颜色
            /// </summary>
            public readonly static Color 乌色 = Color.FromArgb(114, 94, 130);

            /// <summary>
            /// 乌黑：深黑；漆黑
            /// </summary>
            public readonly static Color 乌黑 = Color.FromArgb(57, 47, 65);

            /// <summary>
            /// 漆黑：非常黑的
            /// </summary>
            public readonly static Color 漆黑 = Color.FromArgb(22, 24, 35);

            /// <summary>
            /// 墨色：即黑色
            /// </summary>
            public readonly static Color 墨色 = Color.FromArgb(80, 97, 109);

            /// <summary>
            /// 墨灰：即黑灰
            /// </summary>
            public readonly static Color 墨灰 = Color.FromArgb(117, 138, 153);

            /// <summary>
            /// 黑色：亮度最低的非彩色的或消色差的物体的颜色；最暗的灰色；
            /// 与白色截然不同的消色差的颜色；
            /// 被认为特别属于那些既不能反射、又不能透过能使人感觉到的微小入射光的物体，任何亮度很低的物体颜色
            /// </summary>
            public readonly static Color 黑色 = Color.FromArgb(0, 0, 0);

            /// <summary>
            /// 缁色：帛黑色
            /// </summary>
            public readonly static Color 缁色 = Color.FromArgb(73, 49, 49);

            /// <summary>
            /// 煤黑、象牙黑：都是黑，不过有冷暖之分
            /// </summary>
            public readonly static Color 煤黑 = Color.FromArgb(49, 37, 32);

            /// <summary>
            /// 黧：黑中带黄的颜色
            /// </summary>
            public readonly static Color 黧 = Color.FromArgb(93, 81, 60);

            /// <summary>
            /// 黎：黑中带黄似黎草色
            /// </summary>
            public readonly static Color 黎 = Color.FromArgb(117, 102, 77);

            /// <summary>
            /// 黝：本义为淡黑色或微青黑色
            /// </summary>
            public readonly static Color 黝 = Color.FromArgb(107, 104, 130);

            /// <summary>
            /// 黝黑：皮肤暴露在太阳光下而晒成的，青黑色
            /// </summary>
            public readonly static Color 黝黑 = Color.FromArgb(102, 87, 87);

            /// <summary>
            /// 黯：深黑色、泛指黑色
            /// </summary>
            public readonly static Color 黯 = Color.FromArgb(65, 85, 93);
        }

        public static class 金银色系
        {
            /// <summary>
            /// 赤金：足金的颜色
            /// </summary>
            public readonly static Color 赤金 = Color.FromArgb(242, 190, 69);

            /// <summary>
            /// 金色：平均为深黄色带光泽的颜色
            /// </summary>
            public readonly static Color 金色 = Color.FromArgb(234, 205, 118);

            /// <summary>
            /// 银白：带银光的白色
            /// </summary>
            public readonly static Color 银白 = Color.FromArgb(233, 231, 239);

            /// <summary>
            /// 铜绿
            /// </summary>
            public readonly static Color 铜绿 = Color.FromArgb(84, 150, 136);

            /// <summary>
            /// 乌金
            /// </summary>
            public readonly static Color 乌金 = Color.FromArgb(167, 142, 68);

            /// <summary>
            /// 老银：金属氧化后的色彩
            /// </summary>
            public readonly static Color 老银 = Color.FromArgb(186, 202, 198);
        }
    }
}

