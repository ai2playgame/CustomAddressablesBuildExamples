# Addressablesのビルドグループを作ってそのグループ別にビルドするスクリプト

アセットグループに「ビルドグループ」という追加のSchemaを用意する。

用意したビルドグループは、「GroupA/GroupB/Debug/IncludedInAll」の4つ。

「GroupA With Debug Build」を叩けば、「GroupA / Debug / IncludedInAll」だけがビルドされ、「GroupB」はビルド対象から外される。

## 動作検証環境

Addressables ver.1.19.19

## プロジェクト導入のために必要な作業

Addressablesパッケージを改造しないと動かない。

`BuidScriptPackedMode.cs`に記述されている、`internal void InitalizeBuildContex()` を `public` に変更しないと動かない。
