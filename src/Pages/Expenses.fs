[<RequireQualifiedAccess>]
module Pages.Expenses

open System
open Sutil
open Sutil.Attr
open Sutil.Transition
open Types
open Fable.Core.JS

type private ExpenseKind =
  | Expense of Expense
  | Partial of
    {| name: string
       dueTo: DateTimeOffset
       amount: decimal |}

type private State =
  { expenses: Expense array
    currentExpense: Expense option
    error: string option }

type private Msg =
  | SaveExpense of ExpenseKind
  | FetchExpenses
  | FetchSuccess of Expense array
  | Failed of exn

let private update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | SaveExpense kind -> state, Cmd.none
  | FetchExpenses ->
      state,
      Cmd.OfPromise.either
        Database.getExpensesByDateAndAmount
        (None, None)
        FetchSuccess
        Failed
  | FetchSuccess expenses -> { state with expenses = expenses }, Cmd.none
  | Failed ex ->
      console.log (ex)
      state, Cmd.none

let private init () =
  { currentExpense = None
    expenses = [||]
    error = None },
  Cmd.ofMsg FetchExpenses

let expenseCard (expense: IObservable<Expense>) =
  let expense = Store.current expense

  Html.li [
    Html.details [
      Html.summary [
        text $"""$%.2f{expense.amount} - {expense.dueTo.ToString("D")}"""
      ]
      Html.p [ text expense.name ]
    ]
  ]


let private expenseForm (expense: Expense option) =
  let partial =
    match expense with
    | Some expense ->
        {| name = expense.name
           _id = Some expense._id
           rev = expense.rev
           dueTo = expense.dueTo
           amount = expense.amount |}
    | None ->
        {| name = ""
           _id = None
           rev = None
           dueTo = DateTimeOffset.UtcNow.AddDays(1.0)
           amount = 0.0 |}

  let expense = Store.make (partial)
  let name = expense .> fun expense -> expense.name
  let amount = expense .> fun expense -> expense.amount
  let dueTo = expense .> fun expense -> expense.dueTo

  Html.form [
    Html.article [
      class' "grid"
      Html.section []
      Html.section []
    ]
    Html.footer [
      class' "grid"
      Html.button [
        type' "submit"
        text "Save Expense"
      ]
    ]
  ]

let view () =
  let state, dispatch = Store.makeElmish init update ignore ()

  Html.article [
    class' "page"
    Html.h1 [ text "Expenses!" ]
    Html.section [
      bindFragment (state .> fun state -> state.currentExpense) expenseForm
    ]
    Html.ul [
      eachiko
        (state
         .> fun state -> state.expenses |> List.ofArray)
        (snd >> expenseCard)
        (snd >> id)
        [ slide |> withProps [ Duration 1234. ] |> InOut ]
    ]
    Html.button [
      text "trysavesomething"
      onClick
        (fun _ ->
          Database.saveExpense
            { _id = "41512"
              rev = None
              name = "sample"
              amount = 132.123234
              dueTo = System.DateTimeOffset.UtcNow }
          |> ignore)
        []
    ]
  ]
