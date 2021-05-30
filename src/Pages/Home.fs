[<RequireQualifiedAccess>]
module Pages.Home

open Sutil
open Sutil.DOM
open Sutil.Attr

open Types
open Sutil.Styling
open Components.Shoelace

let view () =

  Html.article [
    class' "page"
    Html.section [
      Html.header [
        Html.div [ text "Expenses" ]
        Html.div [
          class' "icon"
          Shoelace.SlIcon("box-arrow-up-right")
        ]
      ]
    ]
    Html.section [
      Html.header [
        Html.div [ text "Payments" ]
        Html.div [
          class' "icon"
          Shoelace.SlIcon("box-arrow-up-right")
        ]
      ]
    ]
  ]
