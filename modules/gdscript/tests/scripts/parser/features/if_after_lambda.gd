# https://github.com/scardotengine/scardot/issues/61231

func test():
	var my_lambda = func():
		print("hello")
	if 0 == 0:
		my_lambda.call()
