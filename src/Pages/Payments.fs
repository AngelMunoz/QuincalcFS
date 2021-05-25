[<RequireQualifiedAccess>]
module Pages.Payments

open Sutil

let view () =
  Html.article [
    Html.h1 [ text "Payments!" ]
  ]
