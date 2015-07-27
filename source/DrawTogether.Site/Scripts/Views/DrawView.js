function DrawView(hub, whiteboardId, userName, $canvas)
{
    var self = this;
    var figures = new Array();
    var currentFigure = null;
    var isMouseDown;
    var $canvas = $('#canvas');
    var context = $canvas[0].getContext("2d");
    var $usersList = $('#usersList');

    $canvas.mousedown(mouseDownHandler);
    $canvas.mousemove(mouseMoveHandler);
    $canvas.mouseup(mouseUpHandler);
    $canvas.mouseleave(mouseUpHandler);

    self.redraw = function () {
        context.clearRect(0, 0, context.canvas.width, context.canvas.height); // Clears the canvas
        //context.fillStyle = "#e0e0e0";
        //context.fillRect(0, 0, context.canvas.width, context.canvas.height);

        context.strokeStyle = "#df4b26";
        context.lineJoin = "round";
        context.lineWidth = 5;

        for (var figureIndex in figures) {
            var figure = figures[figureIndex];

            context.beginPath();
            console.log("beginpath");

            for (var vertexIndex in figure.Vertices) {
                var vertex = figure.Vertices[vertexIndex];
                console.log("vertex " + vertexIndex + " x:" + vertex.X + " y:" + vertex.Y);

                if (vertexIndex == 0)
                    context.moveTo(vertex.X, vertex.Y);

                context.lineTo(vertex.X, vertex.Y);
            }

            context.stroke();
        }
    }

    self.attachUser = function (userName) {
        $('#usersList').append($('<li />').text(htmlEncode(userName)));
    }

    self.detachUser = function (userName) {
    }

    self.addFigure = function (figure) {
        figures.push(figure);
        self.redraw();
    }

    function beginFigure(x, y) {
        currentFigure = new Figure("Polygon", userName, "#ff000000", new Array());
        figures.push(currentFigure);
        console.log("beginfigure");
    }

    function addVertex(x, y) {
        if (!currentFigure)
            beginFigure(x, y);

        currentFigure.Vertices.push(new Vertex(x, y))
    }

    function endFigure() {
        if (currentFigure && currentFigure.Vertices.length > 0) {
            hub.server.addFigure(currentFigure)
                .done(function (r) { console.log("done"); })
                .fail(function (e) { console.log("fail"); });
        }

        currentFigure = null;
        console.log("endfigure");
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
