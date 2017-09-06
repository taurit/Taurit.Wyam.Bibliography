# Wyam.Bibliography

## About the project
This project contains a custom module for Wyam that helps me build my blog. It's created to allow better manage bibliographic records (understood as all references to external resources that were mentioned in article).

## How it works?

### Defining references

To define a bibliographic reference, author can use markup similar to:

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
    translator="Małgorzata Guzowska"
/> 
```

which will be transformed into a HTML code containing a link to the element in a reference list:

```html
<a href="#reference-list-1" />[1]</a>
```

### Listing references

List of all references can be displayed with a code:

```html
<bibliography
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

[Harvard reference style](https://www.ntnu.edu/viko/harvard-examples) is used to render reference list (here's [another guide on this style](https://library.apiit.edu.my/pdf/harvard-reference-style/Quick_Harvard_Referencing_Guide%20_Revised_16May2016_(4).pdf)).


## Project status

I created this project for my own needs.

However, if you need such tool, feel free to fork it. You can also [create issues](https://github.com/taurit/Wyam.Bibliography/issues) for bugs or change requests.
