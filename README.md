# Wyam.Bibliography

## About the project
This project contains a custom module for Wyam that helps me build my blog. It's created to allow better manage bibliographic records (understood as all references to external resources that were mentioned in article).

The project is discontinued because I no longer use Wyam in any of my projects. I'm leaving it for reference as a public archive, perhaps it can help someone trying to build a similar solution.

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
<a href="#reference-list-1" class="resource-reference" />(Duhigg, 2012, p.123)</a>
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

## How do I integrate with Wyam?

I add the following lines at the bottom of my `config.wyam` file using Blog recipe.

```CS
#assembly d:\Projekty\Wyam.Bibliography\Wyam.Bibliography\bin\Debug\Wyam.Bibliography.dll
Pipelines[Blog.RenderBlogPosts].InsertBefore("WriteFiles", Bibliography());
```

## Project status

I created this project for my own needs. 

Generating bibliography is a problem that is easy to solve for limited number of scenarios. Making this solution support dozens of other work types that can be cited requires more time.

Currently I have no need to implement all those scenarios, as I only need two: citing books and websites.

However, you can [create issues](https://github.com/taurit/Wyam.Bibliography/issues) for bugs or change requests if you would like to see something implemented that would be useful for you :)
