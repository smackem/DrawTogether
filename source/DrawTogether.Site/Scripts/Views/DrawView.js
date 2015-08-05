function DrawView(hub, whiteboardId, userName) {
    var self = this;
    var figures = new Array();
    var currentFigure = null;
    var isMouseDown;
    var $canvas = $('#canvas');
    var context = $canvas[0].getContext("2d");
    var $usersList = $('#usersList');
    var $swatches = $('#colorsList > .swatch');

    $canvas.mousedown(mouseDownHandler);
    $canvas.mousemove(mouseMoveHandler);
    $canvas.mouseup(mouseUpHandler);
    $canvas.mouseleave(mouseUpHandler);

    $swatches.click(swatchClickHandler);

    self.redraw = function () {
        context.clearRect(0, 0, context.canvas.width, context.canvas.height); // Clears the canvas

        for (var figureIndex in figures) {
            var figure = figures[figureIndex];

            context.strokeStyle = figure.Color;
            context.lineJoin = "round";
            context.lineWidth = 5;

            context.beginPath();

            for (var vertexIndex in figure.Vertices) {
                var vertex = figure.Vertices[vertexIndex];

                if (vertexIndex == 0)
                    context.moveTo(vertex.X, vertex.Y);

                context.lineTo(vertex.X, vertex.Y);
            }

            context.stroke();
        }
    }

    self.attachUser = function (userName) {
        $usersList.append($('<div />')
            .text(htmlEncode(userName))
            .attr("data-tag", userName));
    }

    self.detachUser = function (userName) {
        console.log("detachUser");
        $("#usersList > [data-tag='" + userName + "']").remove();
    }

    self.addFigure = function (figure) {
        figures.push(figure);
        self.redraw();
    }

    self.downloadFigures = function () {
        hub.server.getFigures().then(function (remoteFigures) {
            for (var figureIndex in remoteFigures)
                figures.push(remoteFigures[figureIndex]);

            self.redraw();
        })
    }

    function beginFigure(x, y) {
        currentFigure = new Figure("Polygon", userName, "#000000", new Array());
        figures.push(currentFigure);
    }

    function addVertex(x, y) {
        if (!currentFigure)
            beginFigure(x, y);

        currentFigure.Vertices.push(new Vertex(x, y));
    }

    function endFigure() {
        if (currentFigure && currentFigure.Vertices.length > 0) {
            hub.server.addFigure(currentFigure);
        }

        currentFigure = null;
    }

    function mouseDownHandler(e) {
        isMouseDown = true;
        addVertex(e.offsetX, e.offsetY);
        self.redraw();
    }

    function mouseMoveHandler(e) {
        if (isMouseDown) {
            addVertex(e.offsetX, e.offsetY);
            self.redraw();
        }
    }

    function mouseUpHandler(e) {
        isMouseDown = false;
        endFigure();
    }

    function swatchClickHandler(e) {
    }

    function htmlEncode(value) {
        return $('<div />').text(value).html();
    }
}

function Vertex(x, y) {
    var self = this;
    self.X = x;
    self.Y = y;
}

function Figure(kind, userName, color, vertices) {
    var self = this;
    self.Kind = kind;
    self.UserName = userName;
    self.Color = color;
    self.Vertices = vertices;
}
