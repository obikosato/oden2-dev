# oden-dev

## 概要

このプロジェクトは、横浜アリーナのイベント情報をWebサイトから取得し、LINEを通じて通知を送信するアプリケーションです。
Blazor Serverを使用したフロントエンド、ASP.NET Coreを使用したバックエンド、そしてWebスクレイピングやLINE通知のためのPythonスクリプトで構成されています。

## 機能

- 横浜アリーナのWebサイトからイベント情報をスクレイピング。
- LINEユーザーに通知を送信。
- 登録・解除のためのユーザーインターフェースを提供。

## 必要条件

- **データベース**: MySQL
- **バックエンド**: ASP.NET Core
- **フロントエンド**: Blazor Server
- **Python依存関係**:
  - `requests`
  - `bs4` (BeautifulSoup)

## セットアップ

1. **データベース**:
   - MySQLをインストール。
   - `Common/Tools/Database/OdenDbContext.cs` 内の接続文字列を設定。

2. **バックエンド**:
   - .NET SDKをインストール。
   - 以下のコマンドでプロジェクトをビルドおよび実行:

     ```bash
     dotnet build
     dotnet run
     ```

3. **フロントエンド**:
   - 設定されたURLでBlazor Serverアプリケーションにアクセス。

4. **Pythonスクリプト**:
   - 必要な依存関係をインストール:

     ```bash
     pip install requests beautifulsoup4
     ```

   - `GetEventInfoJson.py` を実行してイベントデータをスクレイピング。
   - `LineMessenger.py` を使用してLINE通知を送信:

     ```bash
     python LineMessenger.py <トークン> <メッセージ>
     ```

## 使用方法

- アプリケーションを起動し、ユーザーインターフェースにアクセス。
- 提供されたページで通知の登録または解除を行う。

## ライセンス

このプロジェクトはMITライセンスの下で提供されています。
