module Components.Icon

open Sutil
open Sutil.Svg
open Sutil.Styling
open Sutil.DOM

[<RequireQualifiedAccess>]
type Icon =
  | OpenNew
  | Send
  | Delete
  | Back


  static member d(icon: Icon) =
    match icon with
    | OpenNew ->
        "M14,3V5H17.59L7.76,14.83L9.17,16.24L19,6.41V10H21V3M19,19H5V5H12V3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V12H19V19Z"
    | Send -> "M2,21L23,12L2,3V10L17,12L2,14V21Z"
    | Delete ->
        "M19,4H15.5L14.5,3H9.5L8.5,4H5V6H19M6,19A2,2 0 0,0 8,21H16A2,2 0 0,0 18,19V7H6V19Z"
    | Back ->
        "M20,11V13H8L13.5,18.5L12.08,19.92L4.16,12L12.08,4.08L13.5,5.5L8,11H20Z"




let MdiIcon (icon: Icon) =
  Html.span [
    svg [
      Attr.style "width:24px;height:24px"
      Attr.custom ("viewBox", "0 0 24 24")
      svgel
        "path"
        [ Attr.custom ("color", "currentColor")
          Attr.custom ("d", Icon.d icon) ]
    ]
  ]
  |> withStyle [
       rule "span" [ Css.cursorPointer ]
     ]
