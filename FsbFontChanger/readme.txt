Fallout Shelter 日本語化支援キット（日本語用フォント座標ファイル作成）

このツールは、UnityEXで export したフォント座標ファイルから日本語用フォント座標ファイルを作成するコマンドラインツールです。

使い方：
日本語用フォント座標ファイルを作成する。
  usage: FsbFontChanger.exe -i <original font file path> -o <japanized font file path> -x <xml font path> [-r]
OPTIONS:
  -i, --in=VALUE             オリジナル版のフォント座標ファイルのパスを指定する。
  -o, --out=VALUE            日本語化されたフォント座標ファイルのパスを指定する。
  -x, --xml=VALUE            BMFontで作成された座標ファイル（XML）のパス名を指定する。
  -r                         出力用言語ファイルが既に存在する場合はを上書きする。
  -h, --help                 ヘルプ

Example:
  オリジナルのフォント座標ファイル(-i)とBMFontで作成されたXMLファイル(-x)から日本語用フォント座標ファイル(-o)を作成する 。
    FsbFontChanger.exe -i original\resources_00001.114 -o new\resources_00001.114 -x futura_0.xml
終了コード:
 0  正常終了
 1  異常終了