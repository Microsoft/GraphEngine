# Graph Engine - Open Source

[![Build Status](http://ci.graphengine.io/job/graphengine-master-win/badge/icon)](http://ci.graphengine.io/job/graphengine-master-win)

Microsoft [Graph Engine](http://www.graphengine.io/) is a distributed
in-memory data processing engine, underpinned by a strongly-typed
in-memory key-value store and a general distributed computation
engine.

This repository contains the source code of Graph Engine and its graph
query language -- <a
href="https://www.graphengine.io/video/likq.video.html"
target="_blank">Language Integrated Knowledge Query</a> (LIKQ).
[LIKQ](https://github.com/Microsoft/GraphEngine/tree/master/src/LIKQ)
is a versatile graph query language on top of Graph Engine. It
combines the capability of fast graph exploration and the flexibility
of lambda expression: server-side computations can be expressed in
lambda expressions, embedded in LIKQ, and executed on the server side
during graph traversal.  LIKQ is powering [Academic Graph Search
API](https://www.microsoft.com/cognitive-services/en-us/Academic-Knowledge-API/documentation/GraphSearchMethod),
which is part of Microsoft Cognitive Services.

## Downloads

Graph Engine is regularly released with bug fixes and feature enhancements.

You can install Graph Engine Visual Studio Extension by searching
"Graph Engine" in Visual Studio _Extensions and Updates_
(recommended). It can also be downloaded from <a
href="https://visualstudiogallery.msdn.microsoft.com/12835dd2-2d0e-4b8e-9e7e-9f505bb909b8" target="_blank">Visual
Studio Gallery</a>.

NuGet packages <a
href="https://www.nuget.org/packages/GraphEngine.Core/"
target="_blank">Graph Engine Core</a> and <a
href="https://www.nuget.org/packages/GraphEngine.LIKQ/"
target="_blank">LIKQ</a> are available in the NuGet Gallery.

## How to Contribute

Pull requests, issue reports, and suggestions are welcome.

If you are interested in contributing to the code, please fork the
repository and submit pull requests to the `master` branch.

Please submit bugs and feature requests in [GitHub Issues](https://github.com/Microsoft/GraphEngine/issues).

# License

Copyright (c) Microsoft Corporation. All rights reserved.

Licensed under the [MIT](LICENSE.md) License.