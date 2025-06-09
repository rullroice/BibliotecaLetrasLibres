# 📚 Documentación del Proyecto: Web API RESTful para Biblioteca Municipal “Letras Libres”

- **Nombres:** Vicente Lizana, Raúl Ibarra y Jhon Jaire  
- **Fecha:** 10-06-2025

---

## 📌 Índice

1. [Contexto Inicial](#contexto-inicial)  
2. [Evidencia en Postman](#evidencia-en-postman)  
   - [Libros - CRUD](#libros---crud)  
   - [Usuarios - CRUD](#usuarios---crud)  
   - [Préstamos - CRUD](#préstamos---crud)  
3. [Necesidades y Requerimientos](#necesidades-y-requerimientos)  
4. [Errores Encontrados y Descripción](#errores-encontrados-y-descripción)  
   - [Modelos](#en-los-modelos)  
   - [Controladores](#en-los-controladores)  
5. [Corrección de Errores](#corrección-de-errores)  
6. [Conclusión](#conclusión)  

---

## Contexto Inicial

La Biblioteca Municipal “Letras Libres” utiliza actualmente un sistema manual basado en hojas de cálculo para la gestión de libros, usuarios y préstamos. Esto ocasiona ineficiencias, falta de escalabilidad y riesgo de pérdida o inconsistencia de datos. Por esta razón, se contrató a nuestro equipo para desarrollar una Web API RESTful usando ASP.NET Core y Entity Framework Core que permita digitalizar y modernizar estos procesos, sentando las bases para futuras aplicaciones móviles y web.

---

## 📸 Evidencia en Postman

### Libros - CRUD

**1. Obtener todos los libros**  
![GET-Libros](https://github.com/user-attachments/assets/222d7b14-3d10-4806-9aa6-1ff008cf9d58)

**2. Crear un nuevo libro**  
![POST-Libros](https://github.com/user-attachments/assets/514e508f-0b91-432b-970f-bd9f3315c081)

**3. Consultar libro por ID**  
![GET-ID-Libros](https://github.com/user-attachments/assets/c7b76087-31ea-49d4-9e38-51fe031ece28)

**4. Editar libro por ID**  
![PUT-Libros](https://github.com/user-attachments/assets/2e88fe38-a569-442e-9c94-600f19c66ef8)

**5. Eliminar libro por ID**  
![DELETE-Libros](https://github.com/user-attachments/assets/dcd6a73a-4591-481c-affe-e13bfc0f15a3)

**6. Consultar libro eliminado (404)**  
![GET-ID-Libros(Eliminado)](https://github.com/user-attachments/assets/e1d5a47a-a19c-4f1c-8212-a5b2ac4f2ddd)


---

### Usuarios - CRUD

**7. Obtener todos los usuarios**  
![GET-Usuarios](https://github.com/user-attachments/assets/a26da060-4a7b-4c8a-a411-06459bf07f74)

**8. Crear nuevo usuario**  
![POST-Usuarios](https://github.com/user-attachments/assets/46e3cdfb-f905-4751-bce5-07c8f4155315)

**9. Consultar usuario por ID**  
![GET-ID-Usuarios](https://github.com/user-attachments/assets/0722b3a3-5c75-475f-a7c0-4ff24405b2e5)

**10. Editar usuario por ID**  
![PUT-Usuario(Agregado Extra)](https://github.com/user-attachments/assets/011ef791-4b82-438a-9f31-80a875ac47b5)

**11. Eliminar usuario por ID**  
![DELETE-Usuarios_(Agregado_Extra)](https://github.com/user-attachments/assets/5e01279b-5971-4c93-b00d-402c8338bebc)

**12. Ver usuarios después de eliminar**  
![GET-Usuarios_(Eliminado)](https://github.com/user-attachments/assets/b3889da1-cbb6-4628-b464-574afb374cad)


---

### Préstamos - CRUD

**13. Crear préstamo**  
![POST-Prestamos](https://github.com/user-attachments/assets/c0dd18f9-c7f8-4f84-97a4-f0dba0552dc5)


**14. Devolver préstamo**  
![POST-ID-PRESTAMO](https://github.com/user-attachments/assets/fd7c0c21-795f-424e-bf37-7a9a0efe6a51)


**15. Ver todos los préstamos**  
![GET-Usuario-Prestamos](https://github.com/user-attachments/assets/ee85d2d3-de01-4222-9570-b7ce9caf16c9)


**16. Ver préstamos de un usuario específico**  
![GET-Prestamos_(Agregado_Extra)](https://github.com/user-attachments/assets/c72511fa-b62e-4a1b-9fd3-43645a593b8c)


---

##  Necesidades y Requerimientos

El sistema debe cubrir los siguientes requisitos funcionales:

* Gestión completa de **Libros** (CRUD).
* Gestión de **Usuarios** (registro y consulta).
* Registro y control de **Préstamos** y **Devoluciones**.
* Uso obligatorio de Entity Framework Core para persistencia con SQL Server.
* Correcta modelación de las relaciones entre entidades (uno-a-muchos).
* Validaciones para garantizar integridad y evitar datos inválidos.
* Documentación y prueba de endpoints mediante Swagger.
* Control de errores con respuestas adecuadas.
* Capacidad para futuras extensiones y mantenibilidad.

---

## Errores Encontrados y Descripción

### En los Modelos

#### Libro.cs — Falta validación obligatoria en `Titulo`
En este caso, el error presentado significa que no se validara si el titulo se encuentra con o sin texto, pudiendo generar no solo confusiones, si no que, tambien, errores a corto o largo plazo, al no haber una corección de por medio, esto podria generar horas de trabajo perdidas, en solucionar el problema desde la base de datos de la biblioteca, o podría llegar a significar evaluar manualmente el inventario fisico de libros.
```csharp
public class Libro
{
    public int Id { get; set; }

    // ❌ ERROR: Falta validación obligatoria
    public string Titulo { get; set; }

    public string Autor { get; set; }

    public bool EstaPrestado { get; set; }

    public ICollection<Prestamo> Prestamos { get; set; }
}
```

#### Libros.cs — Codigo Corregigo
En este caso hemos soluciado la clase "Libro" validando los requisitos de titulo y Autor. Del mismo modo, hemos remplazado la clase, por ejemplo agregando in ISBN (Numero Estandar Internacional de Libro) con el fin de identificar los libros dentro de este caso. al igual que colocando un rango de publicación de cada libro con "AnioPublicacion", al igual que hemos agrado "UnidadesDisponibles" con el fin de validar si un libro se encuentra en stock o no, en caso de que las unidades sean 0, el libro aparecera como "Disponible: false"
```csharp
public class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "El autor es obligatorio")]
    public string Autor { get; set; }

    [Required(ErrorMessage = "El ISBN es obligatorio")]
    public string ISBN { get; set; }

    [Range(1000, 2100, ErrorMessage = "Año de publicación inválido")]
    public int AnioPublicacion { get; set; }

    public int UnidadesDisponibles { get; set; }

    public bool Disponible => UnidadesDisponibles > 0;
}
```
---

#### Usuario.cs — Falta validación obligatoria en `Nombre`
Similar al caso de arriba, en este caso el programa no validara si el usuario en cuestion posee un nombre, al trabjar solo con nombres o ID, esto puede generar confusiones a la hora de querer pedir prestado un libro, ya que se requerira obligatoriamente el nombre más el ID del usuario en cuestion, al no poseer un nombre esto podría generar confusiones o incluso problemas a largo plazo, al no saber que usuario posee cierto ID
```csharp
public class Usuario
{
    public int Id { get; set; }

    // ❌ ERROR: Campo obligatorio sin validación
    public string Nombre { get; set; }

    public ICollection<Prestamo> Prestamos { get; set; }
}
```

#### Usuario.cs - Codigo corregido
En este caso de igual forma, se colocaron validaciones con Required, ya que en este caso se le pediran al usuario su nombre, correo y numero, arrojando un mensaje de errir si es que no se agregan estos valores en este caso, de igual modo, el id se genera automaticamente,

```csharp
public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Telefono { get; set; }
}
```
---

#### Prestamo.cs — Falta validación obligatoria en `FechaPrestamo`
En este caso tenemos errores que no simplemente no se ven a primera vista, si no que, tambien, afecta su controlador, al hacer pruebas, incluso luego de corregido, se encontraban errores y bluques debido a "public Libro Libro", no solamente eso, si no que, se encontraron errores con LibroId y UsuarioId, ya que lo unico que lograba hacer era cambiar la "disponibilidad" de los libros de "True" a "False", incluso si no funcionaba.
```csharp
public class Prestamo
{
    public int Id { get; set; }

    public int LibroId { get; set; }

    public Libro Libro { get; set; }

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; }

    // ❌ ERROR: Campo obligatorio sin validación
    public DateTime FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }
}
```

#### Prestamo.cs — Codigo Corregido
Para su corrección se tuvo que realizar la clase "Prestamo" desde 0, pero, de igual modo, esta se vio beneficiada, teniendo un menor numero de lineas y retirando aquellas funciones que estaban dando error en un inicio.
```csharp
public class Prestamo
{
    public int Id { get; set; }
    public int LibroId { get; set; }  // Solo el ID, sin propiedad de navegación
    public int UsuarioId { get; set; } // Solo el ID, sin propiedad de navegación
    public DateTime FechaPrestamo { get; set; } = DateTime.Now;
    public DateTime? FechaDevolucion { get; set; }
}
```
---

### En los Controladores

#### LibrosController.cs — No valida que el título esté vacío en POST

```csharp
[HttpPost]
public async Task<ActionResult<Libro>> PostLibro(Libro libro)
{
    // ❌ ERROR: No se valida si el título está vacío
    _context.Libros.Add(libro);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
}
```

#### LibrosController.cs — Validado

```csharp
[HttpPost]
public async Task<ActionResult<Libro>> Post(Libro libro)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Validación adicional: ISBN único
    bool isbnExiste = await _context.Libros.AnyAsync(l => l.ISBN == libro.ISBN);
    if (isbnExiste)
        return BadRequest("Ya existe un libro con el mismo ISBN.");

    _context.Libros.Add(libro);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = libro.Id }, libro);
}
```

---

#### LibrosController.cs — Permite eliminar libro aunque esté prestado

```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteLibro(int id)
{
    var libro = await _context.Libros.FindAsync(id);
    if (libro == null)
        return NotFound();

    // ❌ ERROR: Se permite eliminar aunque esté prestado
    _context.Libros.Remove(libro);
    await _context.SaveChangesAsync();
    return NoContent();
}
```

#### LibrosController.cs — Validado

```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var libro = await _context.Libros.FindAsync(id);
    if (libro == null) return NotFound();

    // Verifica si el libro tiene préstamos activos (sin fecha de devolución)
    bool estaPrestado = await _context.Prestamos
        .AnyAsync(p => p.LibroId == id && p.FechaDevolucion == null);
    
    if (estaPrestado)
    {
        return BadRequest("No se puede eliminar el libro porque actualmente está prestado.");
    }

    _context.Libros.Remove(libro);
    await _context.SaveChangesAsync();
    return Ok("Libro Eliminado correctamente.");
}
```
---

#### PrestamosController.cs — No verifica existencia de libro o usuario en POST préstamo

```csharp
[HttpPost]
public async Task<IActionResult> Post(Prestamo prestamo)
{
    // ❌ ERROR INTENCIONAL: No se verifica existencia de libro o usuario
    _context.Prestamos.Add(prestamo);
    await _context.SaveChangesAsync();

    return Ok(prestamo);
}

```

#### PrestamosController.cs — Validado

```csharp
[HttpPost]
public async Task<ActionResult<Prestamo>> CrearPrestamo([FromBody] PrestamoRequest request)
{
    // Validar modelo
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Verificar libro
    var libro = await _context.Libros.FindAsync(request.LibroId);
    if (libro == null)
        return NotFound("Libro no encontrado");

    if (libro.UnidadesDisponibles <= 0)
        return BadRequest("Libro no disponible");

    // Verificar usuario
    if (!await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId))
        return NotFound("Usuario no encontrado");

    // Crear préstamo
    var prestamo = new Prestamo
    {
        LibroId = request.LibroId,
        UsuarioId = request.UsuarioId,
        FechaPrestamo = DateTime.Now
    };

    // Actualizar unidades
    libro.UnidadesDisponibles--;

    _context.Prestamos.Add(prestamo);
    await _context.SaveChangesAsync();

    return Ok(prestamo);
}
```

---

#### UsuariosController.cs — No valida que el nombre sea obligatorio en POST

```csharp
[HttpPost]
public async Task<IActionResult> Post(Usuario usuario)
{
    _context.Usuarios.Add(usuario);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
}
```

#### UsuariosController.cs — Validado

```csharp
[HttpPost]
public async Task<ActionResult<Usuario>> Post(Usuario usuario)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Validación adicional: evitar usuarios con email duplicado
    bool emailExiste = await _context.Usuarios
        .AnyAsync(u => u.Email == usuario.Email);

    if (emailExiste)
        return BadRequest("Ya existe un usuario registrado con ese correo electrónico.");

    _context.Usuarios.Add(usuario);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
}
```
---

## Corrección de Errores

Para cada error detectado se aplicaron las siguientes medidas:

* **Validaciones en Modelos:** Se agregaron atributos `[Required]` y otras validaciones a campos obligatorios como `Titulo`, `Nombre` y `FechaPrestamo`.
* **Validaciones en Controladores:** Se incorporaron chequeos para validar entradas, existencia de entidades relacionadas y reglas de negocio antes de operaciones críticas (ej. impedir eliminar libros prestados).
* **Manejo de Estados de Préstamo:** Se agregó lógica para evitar prestar un libro ya prestado y para validar condiciones en devoluciones.
* **Respuesta Adecuada:** Se mejoró el manejo de respuestas HTTP, retornando códigos 400, 404 o 409 según el caso, con mensajes claros.
* **Uso de Include en Consultas:** Para obtener datos relacionados en consultas como el historial de préstamos.

> **Agregar captura/código actualizado para cada corrección.**

---

## Conclusión

Se ha desarrollado una Web API RESTful que cumple con los requisitos funcionales definidos, utilizando ASP.NET Core y Entity Framework Core para la persistencia en SQL Server. El proyecto inicialmente contenía errores comunes relacionados con validaciones insuficientes y reglas de negocio no implementadas, lo que podría provocar inconsistencias y datos inválidos.

Mediante la identificación y corrección de estos errores, el sistema garantiza la integridad y consistencia de la información, mejora la experiencia de usuario y establece una base sólida para futuras ampliaciones. La documentación con Swagger facilita pruebas y mantenimiento.

La entrega incluye el código fuente con comentarios sobre errores intencionales para fines educativos y un plan claro de corrección, aportando un valioso recurso para la comprensión y mejora continua del sistema.
