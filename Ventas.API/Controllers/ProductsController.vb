Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Ventas.Common.Models
Imports Ventas.Domain.Models

Namespace Controllers
    Public Class ProductsController
        Inherits System.Web.Http.ApiController

        Private db As New DataContext

        ' GET: api/Products
        Function GetProducts() As IQueryable(Of Product)
            Return db.Products
        End Function

        ' GET: api/Products/5
        <ResponseType(GetType(Product))>
        Async Function GetProduct(ByVal id As Integer) As Task(Of IHttpActionResult)
            Dim product As Product = Await db.Products.FindAsync(id)
            If IsNothing(product) Then
                Return NotFound()
            End If

            Return Ok(product)
        End Function

        ' PUT: api/Products/5
        <ResponseType(GetType(Void))>
        Async Function PutProduct(ByVal id As Integer, ByVal product As Product) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = product.ProductId Then
                Return BadRequest()
            End If

            db.Entry(product).State = EntityState.Modified

            Try
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateConcurrencyException
                If Not (ProductExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Products
        <ResponseType(GetType(Product))>
        Async Function PostProduct(ByVal product As Product) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Products.Add(product)
            Await db.SaveChangesAsync()

            Return CreatedAtRoute("DefaultApi", New With {.id = product.ProductId}, product)
        End Function

        ' DELETE: api/Products/5
        <ResponseType(GetType(Product))>
        Async Function DeleteProduct(ByVal id As Integer) As Task(Of IHttpActionResult)
            Dim product As Product = Await db.Products.FindAsync(id)
            If IsNothing(product) Then
                Return NotFound()
            End If

            db.Products.Remove(product)
            Await db.SaveChangesAsync()

            Return Ok(product)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function ProductExists(ByVal id As Integer) As Boolean
            Return db.Products.Count(Function(e) e.ProductId = id) > 0
        End Function
    End Class
End Namespace