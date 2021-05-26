[<RequireQualifiedAccess>]
module Pages.Expenses

open Sutil
open Sutil.Attr
open Types

let private expenseForm (expense: Expense option) =
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
  Html.article [
    class' "page"
    Html.h1 [ text "Expenses!" ]
    Html.button [
      text "trysavesomething"
      onClick
        (fun _ ->
          Database.saveExpense
            { _id = "123"
              name = "sample"
              amount = 1001028301982300.1231023012980M
              dueTo = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds() })
        []
    ]
  ]
