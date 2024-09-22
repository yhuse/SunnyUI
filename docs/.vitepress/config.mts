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
          { text: '常见问题', link: '/faq' },
          { text: '安装', link: '/install' },
          { text: '主题', link: '/theme' },
          { text: '国际化', link: '/i18n' },
          { text: '字体图标', link: '/symbol' }
        ]
      },
      {
        text: '多页面框架',
        collapsed: false,
        items: [
          { text: '快速开始', link: '/started' },
          { text: 'DPI缩放自适应方案', link: '/dpi' },
          { text: '全局字体设置', link: '/globalfont' }
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
      pattern: 'https://github.com/vuejs/vitepress/edit/main/docs/:path',
      text: '在 GitHub 上编辑此页面'
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
