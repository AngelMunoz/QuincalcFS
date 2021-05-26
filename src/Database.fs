[<RequireQualifiedAccess>]
module Database

open Fable.Core
open Types

[<ImportMemberAttribute("./Interop/Database.js")>]
let saveExpense (expense: Expense) : unit = jsNative
