[<RequireQualifiedAccess>]
module Database

open Fable.Core
open Types
open System

[<ImportMemberAttribute("./Interop/Database.js")>]
let saveExpense (expense: Expense) : JS.Promise<bool> = jsNative

[<ImportMemberAttribute("./Interop/Database.js")>]
let getExpensesByDateAndAmount
  (
    minDate: DateTimeOffset option,
    maxDate: DateTimeOffset option
  ) : JS.Promise<Expense array> =
  jsNative
