# Wyam.Bibliography

## About the project
This project contains a custom module for Wyam that helps me build my blog. It's created to allow better manage bibliographic records (understood as all references to external resources that were mentioned in article).

## How it works?

### Defining references

To define a bibliographic reference, author of content can use in Markdown files a markup similar to:

```html
<reference
    id="power_of_habit"
    title="The Power fo Habit"
    author="Charles Duhigg"
    url="http://charlesduhigg.com/the-power-of-habit/"
    date="2012"
    edition="1"
    place="Warszawa"
    publisher="Dom Wydawniczy PWN"
    pages="123"
    translator="Małgorzata Guzowska"
/> 
```

which will be transformed into a HTML code containing a link to the element in a reference list:

```html
<a href="#reference-list-1" />(Duhigg, 2012, p.123)</a>
```

### Listing references

List of all references can be displayed with a code:

```html
<reference-list
    id="reference-list"
    header-wrapper="h2"
    header="Reference List"
/>
```

that will be transformed into something like:

```html
<h2 id="reference-list">Reference list</h2>
<ol>
    <li id="#reference-list-1">
        Duhigg, C. (2012) <i>The Power of Habit</i>. 1st edn. Warsaw: Dom Wydawniczy PWN.
    </li>
</ol>
```

[Harvard reference style](https://www.ntnu.edu/viko/harvard-examples) is used to render reference list.

## How do I hook up to Wyam?

I add the following lines at the bottom of my `config.wyam` file using Blog recipe.

```CS
#assembly d:\Projekty\Wyam.Bibliography\Wyam.Bibliography\bin\Debug\Wyam.Bibliography.dll
Pipelines[Blog.BlogPosts].Insert(1, Bibliography());
```

Perhaps this could be done better and avoiding using numeric index, but I don't know how yet.

Additionaly, the following code added to `config.wyam` can be used to test how the pipeline looks after modification:

```CS
foreach (var pipeline in Pipelines[Blog.BlogPosts]) {
	Console.WriteLine(pipeline.ToString());
}

// running wyam outputs:
//
// (...)
// Evaluating configuration script
//     Wyam.Core.Modules.Extensibility.ModuleCollection
//     Wyam.Bibliography.Bibliography
//     Wyam.Core.Modules.Control.Concat
//     Wyam.Html.Excerpt
//     Wyam.Core.Modules.Extensibility.ModuleCollection
//     Wyam.Core.Modules.Metadata.Meta
//     Wyam.Core.Modules.Control.OrderBy
// (...)
```

## Project status

I created this project for my own needs.

However, if you need such tool, feel free to fork it. You can also [create issues](https://github.com/taurit/Wyam.Bibliography/issues) for bugs or change requests.
