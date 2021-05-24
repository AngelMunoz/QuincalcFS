module Pages.Home

open Sutil
open Sutil.DOM
open Sutil.Attr

open Types

type private State = { todos: Todo list }

type private Msg = | Noop

let private init () : State * Cmd<Msg> = { todos = [] }, Cmd.none

let private update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | Noop -> state, Cmd.none


let view () =
  let state, dispatch = Store.makeElmish init update ignore ()

  Html.article [
    disposeOnUnmount [ state ]
    class' "page"
    Html.h1 [ text "Hello, world!" ]
  ]
