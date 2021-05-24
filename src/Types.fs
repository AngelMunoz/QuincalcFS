module Types


open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Browser.Types

type Page =
  | Home
  | Settings

type Theme =
  | Light
  | Dark

type AppSettings = { theme: Theme }

type Todo =
  { userId: int
    id: int
    title: string
    completed: bool }


type FileSystemHandlePermissionDescriptor = {| mode: string option |}

type FileSystemCreateWritableOptions = {| keepExistingData: bool |}

type FileSystemHandle =
  abstract kind : string
  abstract name : string

  abstract isSameEntry : other: FileSystemHandle -> JS.Promise<bool>

  abstract queryPermission :
    descriptor: FileSystemHandlePermissionDescriptor option ->
    JS.Promise<string>

  abstract requestPermission :
    descriptor: FileSystemHandlePermissionDescriptor option ->
    JS.Promise<string>


type FileWithHandle =
  inherit File

  abstract handle : FileSystemHandle option

type FileSystemFileHandle =
  inherit FileWithHandle

  abstract getFile : unit -> JS.Promise<File>

  abstract createWritable :
    ?options: FileSystemCreateWritableOptions ->
    JS.Promise<obj>
