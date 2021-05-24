module App

open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling
open Types
open Components
open Pages
open Stores

type State = { page: Page }

type Msg = NavigateTo of Page
let init () : State * Cmd<Msg> = { page = Home }, Cmd.none

let update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | NavigateTo page -> { state with page = page }, Cmd.none

let navigateTo (dispatch: Dispatch<Msg>) (page: Page) =
  NavigateTo page |> dispatch

let view () =
  let state, elDispatch = Store.makeElmish init update ignore ()
  let navigateTo = navigateTo elDispatch

  Html.app [
    disposeOnUnmount [ state ]
    class' "app-content"
    Html.header [
      Navbar.view (Some navigateTo)
    ]
    Html.main [
      class' "hero is-fullheight"
      bindClass Settings.IsDarkThemeActive "is-dark"
      bindClass Settings.IsLightThemeActive "is-light"
      bindFragment state
      <| fun state ->
           match state.page with
           | Home -> Home.view ()
           | Settings -> Settings.view ()
    ]
  ]
