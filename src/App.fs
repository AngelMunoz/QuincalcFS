module App

open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling
open Types
open Components.Icon
open Pages

type State = { navStack: Page list }

type Msg =
  | NavigateTo of Page
  | NavigateBack

let init () : State * Cmd<Msg> = { navStack = [ Home ] }, Cmd.none


let setContext =
  function
  | Home -> Keyboard.setContext "home"
  | Expenses -> Keyboard.setContext "expenses"
  | Payments -> Keyboard.setContext "payments"
  | Settings -> Keyboard.setContext "settings"


let update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | NavigateTo page ->
      setContext page

      { state with
          navStack = page :: state.navStack },
      Cmd.none
  | NavigateBack ->
      if state.navStack.Length >= 2 then
        let previous = state.navStack.[1..]
        setContext previous.Head
        { state with navStack = previous }, Cmd.none
      else
        state, Cmd.none


let navigateTo (dispatch: Dispatch<Msg>) (page: Page) =
  NavigateTo page |> dispatch


Keyboard.start ()


let view () =
  let state, elDispatch = Store.makeElmish init update ignore ()
  let navigateTo = navigateTo elDispatch

  let enableBack =
    state .> (fun state -> state.navStack.Length <= 1)

  let sub =
    Commands.CommandStream
    |> Store.subscribe (fun cmd -> printfn "%A" cmd)

  Html.app [
    disposeOnUnmount [ state; sub ]
    class' "app-content"
    Html.header [
      Components.Navbar.view (Some navigateTo)
    ]
    Html.main [
      Html.button [
        class' "outline back-btn"
        Attr.custom ("role", "button")
        onClick (fun _ -> elDispatch NavigateBack) []
        MdiIcon(Icon.Back)
        Attr.disabled enableBack
      ]
      bindFragment state
      <| fun state ->
           match state.navStack |> List.head with
           | Home -> Home.view (Some navigateTo)
           | Expenses -> Expenses.view ()
           | Payments -> Payments.view ()
           | Settings -> Settings.view ()
    ]
    |> withStyle [
         rule
           "main"
           [ Css.displayFlex
             Css.flexDirectionColumn ]
         Styles.Page
         rule
           "button.back-btn"
           [ Css.marginLeft Feliz.length.auto
             Css.height 35
             Css.width 75
             Css.displayFlex
             Css.justifyContentCenter
             Css.alignItemsCenter
             Css.marginRight (Feliz.length.em 1)
             Css.custom ("justify-self", "flex-end") ]
       ]
  ]
