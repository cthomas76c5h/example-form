module Login

open Fable.Core
open Thoth.Fetch
open Browser.Dom
open Thoth.Json

// Define a type matching your JSON response.
type LoginResponse = {
    status: string
    csrf_token: string
    token: string
    permissions: string
}

/// Logs in by sending a POST request and storing the token in localStorage.
let login (username: string) (password: string) =
    let loginData = {| username = username; password = password |}
    async {
        // Create a decoder for LoginResponse.
        let decoder : Decoder<LoginResponse> = Decode.Auto.generateDecoder()
        // Use Thoth.Fetch.post which returns a JS.Promise.
        let loginPromise = 
            Fetch.post("http://localhost:8000/login/", loginData, decoder = decoder)
        // Convert the JS.Promise to an F# Async.
        let! loginResult = Async.AwaitPromise loginPromise
        // Save the token to local storage.
        window.localStorage.setItem("token", loginResult.token)
        return loginResult
    }
