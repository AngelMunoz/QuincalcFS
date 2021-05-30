namespace Components.Shoelace

open Sutil

type Shoelace =
  static member SlIcon(name: string) =
    Html.custom ("sl-icon", [ Attr.name name ])
