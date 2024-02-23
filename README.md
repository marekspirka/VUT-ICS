# ICS project - Fast Time Manager

.NET7 app to help with time and project tracking

## Implementation details
- it is possible to have an activity that does not belong to a project
- one activity can have multiple tags

## Known problems
- our git policy got a little bit messy towards the end
    - we created a separate testing branch, which was not the best idea...and we will never do that ever again
- there is one test commented out in every mapper tests file
    - these tests try to map null entity to a model
    - empty model is returned correctly, but the IDs do not match up since the very methods that are called during this process contain Guid.NewGuid()
    - we were not able to find a workaround :(
- seeds are implemented, but not used
    - we ran into some problems with using seeds, so we decided not to use them
    - we plan on changing our tests so that they use these seeds for the 3rd submission

## Authors
- Jan Kuča - xkuca01
- Marek Špirka - xspirk01
- Filip Dvořák - xdvora4c
- Kateřina Lojdová - xlojdo00
- Andrea Michlíková - xmichl11