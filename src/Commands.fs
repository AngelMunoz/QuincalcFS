[<RequireQualifiedAccess>]
module Commands

open Sutil
open Types
open Browser.Types
open Fable.Core.JS

let private CmdStream : IStore<AppCommand option> = Store.make None

let CommandStream =
  CmdStream
  |> Store.filter (fun cmd -> Option.isSome cmd)
  |> Store.map (fun cmd -> cmd.Value)

let Publish (command: AppCommand) = Some command |> Store.set CmdStream

let RegisterShortcut = Keyboard.bindSingleToGlobal

let RegisterShortcuts
  (commands: array<string * (KeyboardEvent -> unit)>)
  (context: string option)
  =
  let context = defaultArg context "global"
  Keyboard.bindToContext commands context



let registerShortcuts () =

  let sendNewPayment (e: KeyboardEvent) : unit =
    AppCommand.New Payment |> Publish

  let sendNewExpense (e: KeyboardEvent) : unit =
    AppCommand.New Expense |> Publish

  RegisterShortcuts [| "ctrl + n", sendNewPayment |] (Some "payments")
  RegisterShortcuts [| "ctrl + n", sendNewExpense |] (Some "expenses")
