[<RequireQualifiedAccess>]
module Pages.Payments

open Sutil
open Sutil.Attr

let view () =
  Html.article [
    class' "page"
    Html.h1 [ text "Payments!" ]
  ]
