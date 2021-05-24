module Pages.Settings

open Sutil
open Sutil.Bulma
open Sutil.DOM
open Sutil.Attr
open Types
open Stores
open Sutil.Styling

let view () =
  let theme = Store.make (Settings.GetTheme())

  let sub =
    Settings.OnThemeChanged(fun updated -> Store.set theme updated)

  Html.article [
    disposeOnUnmount [ theme; sub ]
    class' "page"
    Html.header [ text "Sutil Template" ]
    Html.section [
      class' "content"
      Html.p [
        text "These are your current settings"
        Html.br []
        text "Once you save them we'll use them on the next page load"
      ]
    ]

    Html.section [
      Html.div [
        class' "mb-4"
        bulma.button.button [
          bindClass Settings.IsDarkThemeActive "is-light"
          bindClass Settings.IsLightThemeActive "is-dark"
          button.isOutlined
          bindFragment theme
          <| fun theme ->
               let t =
                 if theme = Light then
                   "Dark"
                 else
                   "Light"

               text $"Switch {t} theme"
          onClick (fun _ -> Settings.SwitchTheme()) []
        ]
      ]
      Html.div [
        class' "is-flex flex-direction-row is-justify-content-space-between"
        bulma.button.button [
          bindClass Settings.IsDarkThemeActive "is-light"
          bindClass Settings.IsLightThemeActive "is-dark"
          button.isOutlined
          text "Save Settings"
          onClick (fun _ -> Settings.SaveSettings()) []
        ]
      ]
    ]
  ]
  |> withStyle [
       rule
         "article"
         [ Css.displayFlex
           Css.flexDirectionColumn
           Css.alignItemsCenter
           Css.justifyContentCenter ]
     ]
