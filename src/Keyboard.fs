[<RequireQualifiedAccess>]
module Keyboard

open Fable.Core
open Browser.Types

[<ImportMember("./Interop/Keyboard.js")>]
let bindToContext
  (bindings: (string * (KeyboardEvent -> unit)) array)
  (ctx: string)
  : unit =
  jsNative

[<ImportMember("./Interop/Keyboard.js")>]
let bindSingleToGlobal
  (keyboardSequence: string)
  (callback: (KeyboardEvent -> unit))
  : unit =
  jsNative

[<ImportMember("./Interop/Keyboard.js")>]
let start () : unit = jsNative

[<ImportMember("./Interop/Keyboard.js")>]
let stop () : unit = jsNative

[<ImportMember("./Interop/Keyboard.js")>]
let setContext (context: string) : unit = jsNative
