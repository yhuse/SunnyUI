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
 * 文件名称: UITexture.cs
 * 文件说明: 主题背景纹理类
 * 当前版本: V3.1
 * 创建日期: 2020-12-10
 *
 * 2020-12-10: V3.0.9 增加文件说明
******************************************************************************/

using System.Drawing;

namespace Sunny.UI
{
    public static class UITexture
    {
        public static TextureBrush CreateTextureBrush(Image img)
        {
            TextureBrush tb = new TextureBrush(img);
            tb.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
            return tb;
        }
    }
}
