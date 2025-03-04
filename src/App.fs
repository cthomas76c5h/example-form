module App

open Browser.Dom
open Browser.Types
open Login

// Get the login form element (make sure your HTML has an element with id="login-form")
let loginForm = document.querySelector("#login-form") :?> HTMLFormElement

// Get the username and password input elements (ensure these IDs exist in your HTML)
let usernameInput = document.querySelector("#username") :?> HTMLInputElement
let passwordInput = document.querySelector("#password") :?> HTMLInputElement

// Register a submit event handler for the form
loginForm.onsubmit <- fun ev ->
    ev.preventDefault()  // Prevent the default form submission behavior

    // Extract values from input fields
    let username = usernameInput.value
    let password = passwordInput.value

    // Log to the console (optional)
    printfn "Attempting login for user: %s" username

    // Call the login function, ignore its return value, and start the async workflow immediately.
    login username password
    |> Async.Ignore
    |> Async.StartImmediate

    null  // Return null as required for Fable event handlers
