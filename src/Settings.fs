[<RequireQualifiedAccess>]
module Settings

open System
open Sutil
open Types
open Browser.WebStorage
open Browser.Types
open Fable.Core.JS

[<Literal>]
let private SETTINGS_KEY = "settings"

let private opts =
  localStorage.getItem SETTINGS_KEY |> Option.ofObj

let private AppSettings : IStore<AppSettings> =
  let defaultState = { theme = Dark }

  match opts with
  | Some settings ->
      match Thoth.Json.Decode.Auto.fromString<AppSettings> settings with
      | Ok settings -> Store.make settings
      | Result.Error err ->
          eprintfn "We couldn't restore the settings from the environment"
          Store.make defaultState
  | None -> Store.make defaultState

let SaveSettings () =
  let settings = Store.get AppSettings

  let settings =
    Thoth.Json.Encode.Auto.toString (0, settings)

  localStorage.setItem (SETTINGS_KEY, settings)


let IsDarkThemeActive : IObservable<bool> =
  Store.map (fun state -> state.theme = Dark) AppSettings

let IsLightThemeActive : IObservable<bool> =
  Store.map (fun state -> state.theme = Light) AppSettings

let SwitchTheme () : unit =
  let settings = Store.getMap id AppSettings

  let updated =
    match settings.theme with
    | Light -> { settings with theme = Dark }
    | Dark -> { settings with theme = Light }

  Store.set AppSettings updated

let GetTheme () : Theme =
  Store.getMap (fun state -> state.theme) AppSettings

let GetSettings () = Store.get AppSettings

let OnThemeChanged (cb: Theme -> unit) =
  Store.subscribe (fun settings -> cb settings.theme) AppSettings
