[<RequireQualifiedAccess>]
module Pages.Home

open Sutil
open Sutil.DOM
open Sutil.Attr

open Types
open Sutil.Styling
open Components.Icon

let view (navigateTo: Option<Page -> unit>) =
  let onMenuItemClick (page: Page) =
    Option.iter (fun fn -> fn page) navigateTo

  Html.article [
    class' "page"
    Html.section [
      Html.header [
        Html.div [ text "Summary" ]
      ]
    ]
    Html.section [
      Html.header [
        Html.div [ text "Expenses" ]
        Html.div [
          class' "icon"
          MdiIcon(Icon.OpenNew)
          onClick (fun _ -> onMenuItemClick Expenses) []
        ]
      ]
    ]
    Html.section [
      Html.header [
        Html.div [ text "Payments" ]
        Html.div [
          class' "icon"
          MdiIcon(Icon.OpenNew)
          onClick (fun _ -> onMenuItemClick Payments) []
        ]
      ]
    ]
  ]
  |> withStyle [
       Styles.Page
       rule
         ".page"
         [ Css.displayFlex
           Css.overflowXAuto
           Css.width (Feliz.length.vw 100)
           Css.custom ("justify-content", "space-evenly")
           Css.padding 0 ]
       rule
         "section"
         [ Css.width (Feliz.length.vw 33.33)
           Css.displayFlex
           Css.flexDirectionColumn
           Css.marginLeft (Feliz.length.em 1)
           Css.marginRight (Feliz.length.em 1)
           Css.marginTop (Feliz.length.em 1) ]
       rule
         "section header"
         [ Css.displayFlex
           Css.justifyContentSpaceBetween ]
       rule ".icon" [ Css.marginLeft (Feliz.length.auto) ]
     ]
