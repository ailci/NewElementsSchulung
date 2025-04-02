using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public sealed class AuthorNotFoundException(Guid authorId)
    : NotFoundException($"The author with id: {authorId} does NOT exist");