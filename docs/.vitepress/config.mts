import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "SunnyUI",
  description: "A Modern Winform UI",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: '更新日志', link: '/updates' },
      { text: '文档', link: '/introduction' },
      { text: '源码', link: 'https://gitee.com/yhuse/SunnyUI'}
    ],

   sidebar: [
      {
        text: '简介',
        collapsed: false,
        items: [
          { text: '项目说明', link: '/introduction' },
          { text: '更新日志', link: '/updates' },
          { text: '升级指南', link: '/updatesi' },
          { text: '常见问题', link: '/faq' },
          { text: '安装', link: '/install' },
          { text: '主题', link: '/theme' },
          { text: '国际化', link: '/i18n' },
          { text: '字体图标', link: '/symbol' }
        ]
      },
      {
        text: '多页面框架',
        collapsed: true,
        items: [
          { text: '快速开始', link: '/started' },
          { text: 'DPI缩放自适应方案', link: '/dpi' },
          { text: '全局字体设置', link: '/globalfont' }
        ]
      },
      {
        text: '窗体',
        collapsed: true,
        items: [
          { text: 'UIForm', link: '/UIForm' },
          { text: 'UILoginForm', link: '/UILoginForm' }
        ]
      },
      {
        text: '控件',
        collapsed: true,
        items: [
          { text: 'UIAvatar', link: '/UIAvatar' },
          { text: 'UIBattery', link: '/UIBattery' },
          { text: 'UIBreadcrumb', link: '/UIBreadcrumb' },
          { text: 'UIButton', link: '/UIButton' },
          { text: 'UICheckBox', link: '/UICheckBox' },
          { text: 'UICheckBoxGroup', link: '/UICheckBoxGroup' },
          { text: 'UIGroupBox', link: '/UIGroupBox' },
          { text: 'UILabel', link: '/UILabel' },
          { text: 'UILedDisplay', link: '/UILedDisplay' },
          { text: 'UILedLabel', link: '/UILedLabel' },
          { text: 'UILedStopwatch', link: '/UILedStopwatch' },
          { text: 'UILinkLabel', link: '/UILinkLabel' },
          { text: 'UIMarkLabel', link: '/UIMarkLabel' },
          { text: 'UIPanel', link: '/UIPanel' },
          { text: 'UIRadioButton', link: '/UIRadioButton' },
          { text: 'UIRadioButtonGroup', link: '/UIRadioButtonGroup' },
          { text: 'UISwitch', link: '/UISwitch' },
          { text: 'UISymbolButton', link: '/UISymbolButton' },
          { text: 'UISymbolLabel', link: '/UISymbolLabel' },
          { text: 'UITitlePanel', link: '/UITitlePanel' }
        ]
      },
      {
        text: '工具类库',
        collapsed: true,
        items: [
          { text: 'IniFile -  Ini文件读写类', link: '/IniFile' },
          { text: 'IniConfig - ini配置文件类', link: '/IniConfig' },
          { text: 'Json - 简易的Json静态类', link: '/Json' }
        ]
      }
    ],
    
    socialLinks: [
      { icon: 'github', link: 'https://gitee.com/yhuse/SunnyUI' },
      { icon: 'github', link: 'https://github.com/yhuse/SunnyUI' }
    ],
    
    footer: {
      message: '基于 GPL3.0 许可发布',
      copyright: `版权所有 © 2012-${new Date().getFullYear()} SunnyUI.Net`
    },
    
    editLink: {
      pattern: 'https://gitee.com/yhuse/SunnyUI/tree/master/docs/:path',
      text: '在 Gitee 上编辑此页面'
    },

    docFooter: {
      prev: '上一页',
      next: '下一页'
    },

    outline: {
      label: '页面导航'
    },

    lastUpdated: {
      text: '最后更新于',
      formatOptions: {
        dateStyle: 'short',
        timeStyle: 'medium'
      }
    },

    langMenuLabel: '多语言',
    returnToTopLabel: '回到顶部',
    sidebarMenuLabel: '菜单',
    darkModeSwitchLabel: '主题',
    lightModeSwitchTitle: '切换到浅色模式',
    darkModeSwitchTitle: '切换到深色模式'
  }
})
