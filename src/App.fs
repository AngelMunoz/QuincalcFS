module App

open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling
open Types
open Components
open Pages

type State = { page: Page }

type Msg = NavigateTo of Page
let init () : State * Cmd<Msg> = { page = Home }, Cmd.none

let update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | NavigateTo page ->
      match page with
      | Home -> Keyboard.setContext "home"
      | Settings -> Keyboard.setContext "settings"

      { state with page = page }, Cmd.none

let navigateTo (dispatch: Dispatch<Msg>) (page: Page) =
  NavigateTo page |> dispatch


Keyboard.start ()


let view () =
  let state, elDispatch = Store.makeElmish init update ignore ()
  let navigateTo = navigateTo elDispatch

  let sub =
    Commands.CommandStream
    |> Store.subscribe (fun cmd -> printfn "%A" cmd)

  Html.app [
    disposeOnUnmount [ state; sub ]
    class' "app-content"
    Html.header [
      Navbar.view (Some navigateTo)
    ]
    Html.main [
      bindFragment state
      <| fun state ->
           match state.page with
           | Home -> Home.view ()
           | Settings -> Settings.view ()
    ]
  ]
