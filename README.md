# NetApiComparison

[![Build Verification](https://github.com/meyerhenning/NetApiComparison/actions/workflows/build_verification.yml/badge.svg)](https://github.com/meyerhenning/NetApiComparison/actions/workflows/build_verification.yml)

NetApiComparison is a collection of controller-based and minimal APIs written in C# and F#.

The main goal of this repository is to provide examples on how to implement the same data and project structure using different API types. Because of that same structure it should also be helpful in breaking language barriers between C# and F#.

## Endpoints

| Endpoint Group | C# Controller | C# Minimal | F# Controller | F# Minimal |
| :-: | :-: | :-: | :-: | :-: |
| Students | ✔ | ✔ | ✔ | ✔ |
| Teachers | ✔ | ✔ | ✔ | ✔ |
| Universities | ✘ | ✘ | ✘ | ✘ |

## Remarks

### Task and Async

Asynchronous programming in C# is bound to Tasks. <br>
In F#, you can either use Tasks or the FsharpAsync type. <br>

It is more common to work with the Async type, so the F# APIs will be using it too.

> <b>Note:</b> <br> Minimal APIs do not handle the FsharpAsync type correctly. <br>
Thus, the F# Minimal API will be using Tasks until the Async support is updated.