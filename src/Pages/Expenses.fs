[<RequireQualifiedAccess>]
module Pages.Expenses

open Sutil

let view () =
  Html.article [
    Html.h1 [ text "Expenses!" ]
  ]
