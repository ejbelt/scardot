# https://github.com/scardotengine/scardot/issues/73273

func not_called():
    var v
    v=func(): v=1
    in v
