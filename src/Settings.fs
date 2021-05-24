[<RequireQualifiedAccess>]
module Settings

open Fable.Core
open Sutil
open Types
open Browser.WebStorage

[<ImportMember("./Interop/Theme.js")>]
let private registerThemeChangedCb (cb: bool -> unit) = jsNative

[<ImportMember("./Interop/Theme.js")>]
let private isDarkThemeActive () : bool = jsNative


[<ImportMember("./Interop/Theme.js")>]
let private overrideTheme (theme: Theme option) : unit = jsNative

[<Literal>]
let private SETTINGS_KEY = "settings"

let private opts =
  localStorage.getItem SETTINGS_KEY |> Option.ofObj

let AppSettings : IStore<AppSettings> =
  let defaultState = { theme = None }

  match opts with
  | Some settings ->
      match Thoth.Json.Decode.Auto.fromString<AppSettings> settings with
      | Ok settings ->
          overrideTheme settings.theme
          Store.make settings
      | Result.Error err ->
          eprintfn "We couldn't restore the settings from the environment"
          Store.make defaultState
  | None -> Store.make defaultState

let SaveSettings () =
  let settings = Store.get AppSettings

  let settings =
    Thoth.Json.Encode.Auto.toString (0, settings)

  localStorage.setItem (SETTINGS_KEY, settings)

let DidSaveTheme () =
  match opts with
  | Some settings ->
      match Thoth.Json.Decode.Auto.fromString<AppSettings> settings with
      | Ok settings -> Option.isSome settings.theme
      | Result.Error err ->
          eprintfn "We couldn't restore the settings from the environment"
          false
  | None -> false

let private getThemeOrCurrentlyActive (theme: Theme option) =
  theme
  |> Option.defaultValue (
    if isDarkThemeActive () then
      Dark
    else
      Light
  )

let SwitchTheme () : unit =
  let settings = Store.getMap id AppSettings

  let updated =
    match settings.theme with
    | Some Light -> { settings with theme = Some Dark }
    | Some Dark -> { settings with theme = Some Light }
    | _ ->
        match getThemeOrCurrentlyActive None with
        | Light -> { settings with theme = Some Dark }
        | Dark -> { settings with theme = Some Light }

  overrideTheme updated.theme
  Store.set AppSettings updated


let GetTheme () : Theme =
  Store.getMap (fun state -> getThemeOrCurrentlyActive state.theme) AppSettings

let GetSettings () = Store.get AppSettings

let OnThemeChanged (cb: Theme -> unit) =
  Store.subscribe
    (fun settings -> cb (getThemeOrCurrentlyActive settings.theme))
    AppSettings

let OverrideTheme (theme: Theme option) =
  let settings = GetSettings()

  match theme with
  | Some theme ->
      overrideTheme (Some theme)
      Store.set AppSettings { settings with theme = Some theme }
      SaveSettings()
  | None ->
      Store.set AppSettings { settings with theme = None }
      overrideTheme None
      SaveSettings()

let RestoreBrowserTheme () = OverrideTheme None

registerThemeChangedCb
  (fun isDark ->
    let settings = GetSettings()

    let settings =
      { settings with
          theme = if isDark then Some Dark else Some Light }

    Store.set AppSettings settings)
