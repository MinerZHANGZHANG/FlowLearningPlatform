# FlowLearningPlatform

#### 简介

FlowLearningPlatform是一个使用BlazorServer框架开发的学习平台，目标是为用户提供便捷的作业分发和提交功能，并在此基础上扩展公告展示、实时讨论等功能，形成一个复合的线上学习平台。

具体见[**项目文档**](https://z-miner.gitbook.io/learning-platform-introduce/)

![网站页面](https://files.gitbook.com/v0/b/gitbook-x-prod.appspot.com/o/spaces%2FK5SPGJbAEBza0XqXyPJu%2Fuploads%2FnT3q9hoCVYGHyERSbCRT%2F%E6%95%88%E6%9E%9C.png?alt=media&token=7d26190b-e223-4f2c-8f14-580bedf6ab2b)


#### 功能

- 用户系统：使用用户编号和密码进行身份验证，根据用户组别提供不同的操作权限和展示内容。
- 作业系统：教师发布作业，学生进行提交，支持富文本编辑和附件上传。
- 公告系统：教师和管理员可发布公告，在首页等位置展示。

![网站功能](https://files.gitbook.com/v0/b/gitbook-x-prod.appspot.com/o/spaces%2FK5SPGJbAEBza0XqXyPJu%2Fuploads%2FqmbfmJ2XUI2wwPQ5TYnl%2F%E5%AD%A6%E4%B9%A0%E5%B9%B3%E5%8F%B0%E6%A8%A1%E5%9D%97.png?alt=media&token=3529ed24-f9f3-4b2e-b555-d6ca3a1e909f)


#### 使用

1. 下载安装对应的版本

2. 配置数据库连接

打开appsettings.json文件，设置数据库连接字符串(如sqlserver)

`"ConnectionStrings": {
    "DefaultConnection": "server=localhost\\sqlexpress;database=learnPlatform;trusted_connection=true;TrustServerCertificate=true"
  }`

3. 使用ef工具迁移创建数据库

这里使用了捆绑包的方式进行迁移，在命令行下运行efbundle.exe，将会根据连接字符串创建对应数据库

`.\efbundle.exe`

4.运行

`.\FlowLearningPlatform.exe`

#### 计划

- [ ] 添加配置页面
- [ ] 创建docker部署版本
- [ ] 使用SignalR开发实时聊天功能

#### 感谢

- UI框架 [AntDesignBlazor](https://antblazor.com/)
- 富文本编辑 [Blazored.TextEditor](https://github.com/Blazored/TextEditor)
- 单元测试框架 [NUnit](https://nunit.org/)