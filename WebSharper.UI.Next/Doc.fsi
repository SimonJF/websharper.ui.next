﻿// $begin{copyright}
//
// This file is confidential and proprietary.
//
// Copyright (c) IntelliFactory, 2004-2014.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

namespace IntelliFactory.WebSharper.UI.Next

type IPagelet = IntelliFactory.WebSharper.Html.IPagelet

/// Represents a time-varying node or a node list.
type Doc

/// Combinators on documents.
type Doc with

  // Construction of basic nodes.

    /// Constructs a reactive element node.
    static member Element : name: string -> seq<Attr> -> seq<Doc> -> Doc

    /// Same as Element, but uses SVG namespace.
    static member SvgElement : name: string -> seq<Attr> -> seq<Doc> -> Doc

    /// Embeds time-varying fragments.
    static member EmbedView : View<Doc> -> Doc

    /// Creates a Doc using a given DOM element
    static member Static : Element -> Doc

    /// Constructs a reactive text node.
    static member TextView : View<string> -> Doc

  // Note: Empty, Append, Concat define a monoid on Doc.

    /// Empty tree.
    static member Empty : Doc

    /// Append on trees.
    static member Append : Doc -> Doc -> Doc

    /// Concatenation.
    static member Concat : seq<Doc> -> Doc

  // Collections.

    /// Converts a collection to Doc using View.Convert and embeds the concatenated reslut.
    /// Shorthand for View.Convert f |> View.Map Doc.Concat |> Doc.EmbedView.
    static member Convert<'T when 'T : equality> :
        ('T -> Doc) -> View<seq<'T>> -> Doc

    /// Doc.Convert with a custom key.
    static member ConvertBy<'T,'K when 'K : equality> :
        ('T -> 'K) -> ('T -> Doc) -> View<seq<'T>> -> Doc

    /// Converts a collection to Doc using View.ConvertSeq and embeds the concatenated reslut.
    /// Shorthand for View.ConvertSeq f |> View.Map Doc.Concat |> Doc.EmbedView.
    static member ConvertSeq<'T when 'T : equality> :
        (View<'T> -> Doc) -> View<seq<'T>> -> Doc

    /// Doc.ConvertSeq with a custom key.
    static member ConvertSeqBy<'T,'K when 'K : equality> :
        ('T -> 'K) -> (View<'T> -> Doc) -> View<seq<'T>> -> Doc

  // Main entry-point combinators - use once per app

    /// Runs a reactive Doc as contents of the given element.
    static member Run : Element -> Doc -> unit

    /// Same as rn, but looks up the element by ID.
    static member RunById : id: string -> Doc -> unit

    /// Creates an IPagelet from a Doc, in a Div container.
    static member AsPagelet : Doc -> IPagelet

  // Special cases

    /// Static variant of TextView.
    static member TextNode : string -> Doc

  // Form helpers

    /// Input box.
    static member Input : seq<Attr> -> Var<string> -> Doc

    /// Input box.
    static member InputArea : seq<Attr> -> Var<string> -> Doc

    /// Password box.
    static member PasswordBox : seq<Attr> -> Var<string> -> Doc

    /// Submit button. Takes a view of reactive components with which it is associated,
    /// and a callback function of what to do with this view once the button is pressed.
    static member Button : caption: string -> seq<Attr> -> (unit -> unit) -> Doc

    /// Link with a callback, acts just like a button.
    static member Link : caption: string -> seq<Attr> -> (unit -> unit) -> Doc

    /// Check Box Group.
    static member CheckBox<'T when 'T : equality> : ('T -> string) -> list<'T> -> Var<list<'T>> -> Doc

    /// Select box.
    static member Select<'T when 'T : equality> : seq<Attr> -> ('T -> string) -> list<'T> -> Var<'T> -> Doc