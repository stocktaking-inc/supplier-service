CONTAINER_NAME := supplier-service
NETWORK_NAME := stocktaking-net

.PHONY: build run

build:
	docker build -t $(CONTAINER_NAME) .

run:
	docker run -d \
		--name $(CONTAINER_NAME) \
		--network $(NETWORK_NAME) \
		-p 5101:5101 \
		$(CONTAINER_NAME)