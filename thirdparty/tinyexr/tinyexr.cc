#if defined(_WIN32)
#ifndef NOMINMAX
#define NOMINMAX
#endif
#endif

// -- SCARDOT start --
#include <zlib.h> // Should come before including tinyexr.
// -- SCARDOT end --

#define TINYEXR_IMPLEMENTATION
#include "tinyexr.h"
