# FirstDemo — Open Claw 部署指南

本项目是一个基于 Unity 2021.3 的游戏 Demo，支持以下部署方式：
- **微信小游戏**（通过 WX-WASM-SDK-V2）
- **Android APK**
- **Windows 独立应用**
- **WebGL 网页版**

---

## 环境要求

| 依赖 | 版本 |
|------|------|
| Unity Editor | 2021.3.18f1c1 |
| Unity 模块 | WebGL Build Support、Android Build Support |
| 微信开发者工具 | 最新稳定版 |
| Android SDK / JDK | Android Build 所需 |

---

## 一、微信小游戏部署（推荐）

本项目已集成 **WX-WASM-SDK-V2**，可一键将 Unity WebGL 包转换为微信小游戏项目。

### 步骤

1. **打开项目**

   使用 Unity Hub 选择 `2021.3.18f1c1` 版本打开本仓库根目录。

2. **配置 SDK**

   在 Unity 菜单栏点击 **微信小游戏 → 转换小游戏**，填写以下信息：
   - AppID：在 [微信公众平台](https://mp.weixin.qq.com) 注册小游戏后获取
   - 游戏名称：LogTest（或自定义）
   - 导出路径：`BuildExport/`（默认）

3. **执行导出**

   点击"导出 WebGL 并转换"，SDK 会自动完成 Unity WebGL 打包并生成微信小游戏工程到 `BuildExport/` 目录。

4. **用微信开发者工具打开**

   启动[微信开发者工具](https://developers.weixin.qq.com/miniprogram/dev/devtools/download.html)，选择"小游戏"，导入 `BuildExport/` 目录。

5. **预览与上传**

   - 点击"预览"在手机上扫码测试。
   - 测试通过后点击"上传"，在微信公众平台提交审核即可发布。

---

## 二、Android APK 构建与部署

1. **配置 Android Build Support**

   确保 Unity 已安装 Android Build Support 模块，并在 **Edit → Preferences → External Tools** 配置 JDK 和 Android SDK 路径。

2. **切换平台**

   **File → Build Settings → Android → Switch Platform**

3. **构建 APK**

   点击 **Build** 并选择输出路径，或直接使用已构建好的 APK：

   ```
   apk/logTest.apk
   ```

4. **安装到设备**

   ```bash
   adb install apk/logTest.apk
   ```

---

## 三、Windows 独立应用构建（命令行）

项目提供了 `Assets/Editor/BuildTools.cs` 脚本，可通过 Unity 批处理模式构建 Windows 应用：

```bash
Unity.exe -batchmode -quit \
  -projectPath "<项目根目录>" \
  -executeMethod BuildTools.BuildApp \
  -logFile build.log
```

构建产物输出到 `Builds/App.exe`。

---

## 四、WebGL 网页版本地运行

`BuildExport/` 目录中已包含预构建的 WebGL 版本，可直接用静态服务器提供服务：

```bash
# 使用 Python 启动本地服务器（需 Python 3）
cd BuildExport
python -m http.server 8080
```

打开浏览器访问 `http://localhost:8080` 即可运行。

> **注意**：WebGL 版本须通过 HTTP/HTTPS 服务器访问，不可直接双击打开 `index.html`。

---

## 资源管理（YooAsset）

本项目使用 [YooAsset](https://github.com/tuyoogame/YooAsset) 进行资源包管理。
首次运行前需在 Unity Editor 中执行 **YooAsset → Build** 构建资源包，
生成文件位于 `Bundles/` 目录，部署时需随应用一并打包或上传到 CDN。

---

## 常见问题

**Q: 微信开发者工具提示"代码包超限"怎么办？**

A: 在 WX-WASM-SDK-V2 导出设置中开启"首包资源优化"，将大文件移至远程 CDN，并配置 YooAsset 远程资源地址。

**Q: Android 构建报错找不到 JDK？**

A: 在 Unity **Edit → Preferences → External Tools** 中手动指定 JDK 路径，推荐使用 JDK 11。

**Q: WebGL 运行白屏？**

A: 检查 `BuildExport/Build/` 目录下 `.wasm`、`.data`、`.framework.js` 和 `.loader.js` 四个文件是否完整。